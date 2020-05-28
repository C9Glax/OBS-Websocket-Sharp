using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;

namespace OBSWebsocketSharp
{
    public partial class OBSConnector
    {
        private readonly WebSocket socket;
        private readonly string password;
        private ulong messageId = 0;
        private readonly Dictionary<ulong, TaskCompletionSource<JObject>> messages;


        public OBSConnector(string url, string password)
        {
            this.messages = new Dictionary<ulong, TaskCompletionSource<JObject>>();
            this.password = password;
            this.socket = new WebSocket(url.Contains("ws://") ? url : "ws://" + url);
            this.socket.OnMessage += OnMessage;
            this.socket.Connect();
            this.Authenticate();
        }

        public void Close()
        {
            this.socket.Close();
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            if (!e.IsText)
                return;
            JObject message = JObject.Parse(e.Data);
            if (message.ContainsKey("status") && message["status"].ToString() == "ok")
            {
                if (message.ContainsKey("message-id") && message["message-id"].ToString() != "")
                    this.messages[Convert.ToUInt64(message["message-id"].ToString())].SetResult(message);
                else
                    this.OnOBSWebsocketWarning?.Invoke(this, new OBSWebsocketEventArgs()
                    {
                        text = "Message-id missing"
                    });
            }
            else if (message.ContainsKey("update-type"))
                this.RaiseEvent(message);
            else
                this.OnOBSWebsocketWarning?.Invoke(this, new OBSWebsocketEventArgs()
                {
                    text = message["error"].ToString()
                });
        }

        private void Authenticate()
        {
            JObject response = this.Request("GetAuthRequired");
            bool authRequired = Convert.ToBoolean((string)response["authRequired"]);
            if (authRequired)
            {
                string challenge = (string)response["challenge"];
                string salt = (string)response["salt"];
                using (SHA256 sha = SHA256.Create())
                {
                    string secretString = password + salt;
                    byte[] secretHash = sha.ComputeHash(Encoding.ASCII.GetBytes(secretString));
                    string secret = Convert.ToBase64String(secretHash);

                    string authResponseString = secret + challenge;
                    byte[] authResponseHash = sha.ComputeHash(Encoding.ASCII.GetBytes(authResponseString));
                    string authResponse = Convert.ToBase64String(authResponseHash);

                    this.Request("Authenticate", "auth", authResponse);
                }
            }
        }

        private JObject Request(string requestType, params object[] parameters)
        {
            JObject requestObject = new JObject
            {
                { "request-type", requestType },
                { "message-id", (++this.messageId).ToString() }
            };
            for (int param = 0; param < parameters.Length; param += 2)
                requestObject.Add((string)parameters[param], JToken.FromObject(parameters[param + 1]));

            if (!this.socket.IsAlive)
                this.OnOBSWebsocketWarning?.Invoke(this, new OBSWebsocketEventArgs()
                {
                    text = "Websocket not Alive!"
                });

            TaskCompletionSource<JObject> tcs = new TaskCompletionSource<JObject>();
            this.messages.Add(this.messageId, tcs);

            this.socket.Send(requestObject.ToString());

            tcs.Task.Wait();
            if (tcs.Task.IsCanceled)
                this.OnOBSWebsocketInfo?.Invoke(this, new OBSWebsocketEventArgs()
                {
                    text = "Request Canceled"
                });

            this.messages.Remove(this.messageId);

            return tcs.Task.Result;
        }
    }
}
