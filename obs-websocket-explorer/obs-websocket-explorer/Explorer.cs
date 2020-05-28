using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using OBSWebsocketSharp;

namespace obs_websocket_explorer
{
    class Explorer
    {
        public Explorer(string ip, string password)
        {
            OBSConnector obsConnector = new OBSConnector(ip, password);
            obsConnector.OnOBSMessageReceived += (s,e) => { Console.WriteLine(e.message); };
            obsConnector.OnOBSWebsocketInfo += (s, e) => { Console.WriteLine(e.text); };
            Console.WriteLine("obs-websocket protocol: https://github.com/Palakis/obs-websocket/blob/4.x-current/docs/generated/protocol.md");
            Console.WriteLine("Write Requests in Format: <RequestName> [<parameterName> <parameterValue>]");

            while (true) //That's nice
            {
                string[] consoleLine = Console.ReadLine().Split(' ');
                if(consoleLine[0].Length > 0)
                {
                    List<object> parameters = new List<object>();
                    for (int i = 1; i < consoleLine.Length; i += 2)
                    {
                        parameters.Add(consoleLine[i]);

                        if (bool.TryParse(consoleLine[i + 1], out _))
                            parameters.Add(bool.Parse(consoleLine[i + 1]));
                        else if (double.TryParse(consoleLine[i + 1], out _))
                            parameters.Add(double.Parse(consoleLine[i + 1]));
                        else if (int.TryParse(consoleLine[i + 1], out _))
                            parameters.Add(int.Parse(consoleLine[i + 1]));
                        else
                            parameters.Add(consoleLine[i + 1]);
                    }
                    JObject response = obsConnector.Request(consoleLine[0], parameters.ToArray());
                    Console.WriteLine(response);
                }
            }
        }

        static void Main(string[] args)
        {
            if(args.Length == 2)
            {
                new Explorer(args[0], args[1]);
            }else if(args.Length == 1)
            {
                new Explorer(args[0], "");
            }else
            {
                Console.WriteLine("obs-websocket address (Press enter for 127.0.0.1:4444):");
                string ip = Console.ReadLine();
                Console.WriteLine("Password (Press Enter if none):");
                string password = Console.ReadLine();
                new Explorer((ip == "") ? "127.0.0.1:4444" : ip, password);
            }
        }
    }
}
