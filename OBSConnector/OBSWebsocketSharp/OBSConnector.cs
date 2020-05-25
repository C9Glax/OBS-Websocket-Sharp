using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace OBSWebsocketSharp
{
    public class OBSConnector
    {
        private readonly WebSocket socket;
        private readonly string password;
        private ulong messageId = 0;
        private readonly Dictionary<ulong, JObject> messages;
        private readonly ushort timeout = 3000;

        public OBSConnector(string url, string password)
        {
            this.messages = new Dictionary<ulong, JObject>();
            this.password = password;
            this.socket = new WebSocket(url.Contains("ws://") ? url : "ws://" + url);
            this.socket.OnOpen += Authenticate;
            this.socket.OnMessage += OnMessage;
            this.socket.Connect();
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsPing)
                return;
            JObject message = JObject.Parse(e.Data);
            if (message["status"].ToString() != "ok")
                throw new Exception(message["error"].ToString());

            if (message["message-id"].ToString() != "")
                this.messages.Add(Convert.ToUInt64(message["message-id"].ToString()), message);
            else
                throw new Exception("Message-id missing");

            Console.WriteLine(message);
        }

        private void Authenticate(object sender, EventArgs e)
        {
            this.SendAuthentication(this.password);
        }

        private void SendAuthentication(string password)
        {
            JObject response = this.Request("GetAuthRequired");
            throw new NotImplementedException();
        }

        private JObject Request(string requestType, params string[] parameters)
        {
            JObject requestObject = new JObject
            {
                { "request-type", requestType },
                { "message-id", (++this.messageId).ToString() }
            };
            for (int param = 0; param < parameters.Length; param += 2)
                requestObject.Add(parameters[param], parameters[param + 1]);

            if (!this.socket.IsAlive)
                throw new Exception("Websocket not alive!");
            this.socket.Send(requestObject.ToString());

            JObject response = WaitForMessage(this.messageId);
            return response;
        }

        private JObject WaitForMessage(ulong id)
        {
            for (ushort timeelapsed = 0; timeelapsed < this.timeout; timeelapsed++)
            {
                if (this.messages.ContainsKey(id))
                {
                    JObject response = this.messages[id];
                    this.messages.Remove(id);
                    return response;
                }
                Thread.Sleep(1);
            }
            throw new TimeoutException("Websocket-server did not respond in time.");
        }
        #region Requests
        public OBSStats GetStats()
        {
            JObject response = this.Request("GetStats");
            return new OBSStats()
            {
                averageframetime = Convert.ToDouble(response["stats"]["average-frame-time"].ToString()),
                cpuusage = Convert.ToDouble(response["stats"]["cpu-usage"].ToString()),
                fps = Convert.ToDouble(response["stats"]["fps"].ToString()),
                freediskspace = Convert.ToDouble(response["stats"]["free-disk-space"].ToString()),
                memoryusage = Convert.ToDouble(response["stats"]["memory-usage"].ToString()),
                outputskippedframes = Convert.ToInt32(response["stats"]["output-skipped-frames"].ToString()),
                outputtotalframes = Convert.ToInt32(response["stats"]["output-total-frames"].ToString()),
                rendertotalframes = Convert.ToInt32(response["stats"]["render-total-frames"].ToString())
            };
        }

        public VideoInfo GetVideoInfo()
        {
            JObject response = this.Request("GetVideoInfo");
            return new VideoInfo()
            {
                baseHeight = Convert.ToInt32((string)response["baseHeight"]),
                baseWidth = Convert.ToInt32((string)response["baseWidth"]),
                colorRange = (string)response["colorRange"],
                colorSpace = (string)response["colorSpace"],
                fps = Convert.ToDouble((string)response["fps"]),
                outputHeight = Convert.ToInt32((string)response["outputHeight"]),
                outputWidth = Convert.ToInt32((string)response["outputWidth"]),
                scaleType = (string)response["scaleType"],
                videoFormat = (string)response["videoFormat"]
            };
        }

        public Output[] ListOutputs()
        {
            JObject response = this.Request("ListOutputs");
            JToken[] outputs = response["outputs"].ToArray();
            Output[] ret = new Output[outputs.Length];
            for(short i = 0; i < outputs.Length; i++)
            {
                ret[i] = new Output()
                {
                    active = Convert.ToBoolean((string)outputs[i]["active"]),
                    congestion = Convert.ToDouble((string)outputs[i]["congestion"]),
                    droppedFrames = Convert.ToInt32((string)outputs[i]["droppedFrames"]),
                    height = Convert.ToInt32((string)outputs[i]["height"]),
                    width = Convert.ToInt32((string)outputs[i]["width"]),
                    name = (string)outputs[i]["name"],
                    reconnecting = Convert.ToBoolean((string)outputs[i]["reconnecting"]),
                    totalBytes = Convert.ToInt32((string)outputs[i]["totalBytes"]),
                    totalFrames = Convert.ToInt32((string)outputs[i]["totalFrames"]),
                    type = (string)outputs[i]["type"]
                };
            }
            return ret;
        }

        public Output GetOutputInfo(string outputName)
        {
            JObject response = this.Request("GetOutputInfo", "outputName", outputName);
            return new Output()
            {
                active = Convert.ToBoolean((string)response["outputInfo"]["active"]),
                congestion = Convert.ToDouble((string)response["outputInfo"]["congestion"]),
                droppedFrames = Convert.ToInt32((string)response["outputInfo"]["droppedFrames"]),
                height = Convert.ToInt32((string)response["outputInfo"]["height"]),
                width = Convert.ToInt32((string)response["outputInfo"]["width"]),
                name = (string)response["outputInfo"]["name"],
                reconnecting = Convert.ToBoolean((string)response["outputInfo"]["reconnecting"]),
                totalBytes = Convert.ToInt32((string)response["outputInfo"]["totalBytes"]),
                totalFrames = Convert.ToInt32((string)response["outputInfo"]["totalFrames"]),
                type = (string)response["outputInfo"]["type"]
            };
        }

        public void StartOutput()
        {
            _ = this.Request("StartOutput");
        }

        public void StopOutput()
        {
            _ = this.Request("StopOutput");
        }
        public void StopOutput(bool force)
        {
            _ = this.Request("StopOutput", "force", force.ToString());
        }

        public void StartStopRecording()
        {
            _ = this.Request("StartStopRecording");
        }

        public void StartRecording()
        {
            _ = this.Request("StartRecording");
        }

        public void StopRecording()
        {
            _ = this.Request("StopRecording");
        }

        public void PauseRecording()
        {
            _ = this.Request("PauseRecording");
        }

        public void ResumeRecording()
        {
            _ = this.Request("ResumeRecording");
        }

        public void SetRecordingFolder(string recfolder)
        {
            _ = this.Request("SetRecordingFolder", "rec-folder", recfolder);
        }

        public string GetRecordingFolder()
        {
            JObject response = this.Request("GetRecordingFolder");
            return (string)response["rec-folder"];

        }

        public void StartStopReplayBuffer()
        {
            _ = this.Request("StartStopReplayBuffer");
        }

        public void StartReplayBuffer()
        {
            _ = this.Request("StartReplayBuffer");
        }
        public void StopReplayBuffer()
        {
            _ = this.Request("StopReplayBuffer");
        }

        public void SaveReplayBuffer()
        {
            _ = this.Request("SaveReplayBuffer");
        }

        public void SetCurrentSceneCollection(string scname)
        {
            _ = this.Request("SetCurrentSceneCollection", "sc-name", scname);
        }
        public string GetCurrentSceneCollection()
        {
            JObject response = this.Request("GetCurrentSceneCollection");
            return (string)response["sc-name"];
        }

        public string[] ListSceneCollections()
        {
            JObject response = this.Request("ListSceneCollections");
            JToken[] outputs = response["scene-collections"].ToArray();
            string[] ret = new string[outputs.Length];
            for(short i = 0; i < outputs.Length; i++)
                ret[i] = (string)outputs[i];
            return ret;
        }

        public void SetCurrentScene(string scenename)
        {
             _ = this.Request("SetCurrentScene", "scene-name", scenename);
        }

        public string GetCurrentScene()
        {
            JObject response = this.Request("GetCurrentScene");
            return (string)response["name"];
        }

        public string[] GetSceneList()
        {
            JObject response = this.Request("GetSceneList");
            JToken[] outputs = response["scenes"].ToArray();
            string[] ret = new string[outputs.Length];
            for (short i = 0; i < outputs.Length; i++)
                ret[i] = (string)outputs[i]["name"];
            return ret;
        }

        public double GetVolume(string source)
        {
            JObject response = this.Request("GetVolume", "source", source);
            return Convert.ToDouble((string)response["volume"]);
        }

        public void SetVolume(string source, double volume)
        {
            _ = this.Request("SetVolume", "source", source, "volume", volume.ToString());
        }

        public bool GetMute(string source)
        {
            JObject response = this.Request("GetMute", "source", source);
            return Convert.ToBoolean((string)response["muted"]);
        }

        public void SetMute(string source, bool mute)
        {
            _ = this.Request("GetMute", "source", source, "mute", mute.ToString());
        }

        public void ToggleMute(string source)
        {
            _ = this.Request("ToggleMute", "source", source);
        }

        public void SetSyncOffset(string source, int offset)
        {
            _ = this.Request("SetSyncOffset", "source", source, "offset", offset.ToString());
        }

        public int GetSyncOffset(string source)
        {
            JObject response = this.Request("GetSyncOffset", "source", source);
            return Convert.ToInt32((string)response["offset"]);
        }

        public SpecialSources GetSpecialSources()
        {
            JObject response = this.Request("GetSpecialSources");
            return new SpecialSources()
            {
                desktop1 = (string)response["desktop-1"],
                desktop2 = (string)response["desktop-2"],
                mic1 = (string)response["mic-1"],
                mic2 = (string)response["mic-2"],
                mic3 = (string)response["mic-3"],
                mic4 = (string)response["mic-4"],
            };
        }

        public StreamingStatus GetStreamingStatus()
        {
            JObject response = this.Request("GetStreamingStatus");
            return new StreamingStatus()
            {
                previewonly = Convert.ToBoolean((string)response["preview-only"]),
                recording = Convert.ToBoolean((string)response["recording"]),
                rectimecode = (string)response["rec-timecode"],
                streaming = Convert.ToBoolean((string)response["streaming"]),
                streamtimecode = (string)response["stream-timecode"],
            };
        }

        public void StartStopStreaming()
        {
            _ = this.Request("StartStopStreaming");
        }

        public void StartStreaming()
        {
            _ = this.Request("StartStreaming");
        }

        public void StopStreaming()
        {
            _ = this.Request("StopStreaming");
        }
        #endregion


    }
}
