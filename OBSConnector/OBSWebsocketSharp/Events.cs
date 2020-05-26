using Newtonsoft.Json.Linq;
using System;

namespace OBSWebsocketSharp
{
    public partial class OBSConnector
    {

        public event OBSSceneSwitchedEventHandler OnOBSSceneSwitched;
        public delegate void OBSSceneSwitchedEventHandler(object sender, OBSSceneSwitchedEventArgs e);
        public class OBSSceneSwitchedEventArgs : EventArgs
        {
            public string newSceneName;
        }

        public event OBSStreamStartingEventHandler OnOBSStreamStarting;
        public delegate void OBSStreamStartingEventHandler(object sender, OBSStreamEventArgs e);
        public class OBSStreamEventArgs : EventArgs
        {
        }

        public event OBSStreamStartedEventHandler OnOBSStreamStarted;
        public delegate void OBSStreamStartedEventHandler(object sender, OBSStreamEventArgs e);

        public event OBSStreamStoppingEventHandler OnOBSStreamStopping;
        public delegate void OBSStreamStoppingEventHandler(object sender, OBSStreamEventArgs e);

        public event OBSStreamStoppedEventHandler OnOBSStreamStopped;
        public delegate void OBSStreamStoppedEventHandler(object sender, OBSStreamEventArgs e);

        public event OBSRecordingStartingEventHandler OnOBSRecordingStarting;
        public delegate void OBSRecordingStartingEventHandler(object sender, OBSRecordingEventArgs e);
        public class OBSRecordingEventArgs : EventArgs
        {
        }

        public event OBSRecordingStartedEventHandler OnOBSRecordingStarted;
        public delegate void OBSRecordingStartedEventHandler(object sender, OBSRecordingEventArgs e);

        public event OBSRecordingStoppingEventHandler OnOBSRecordingStopping;
        public delegate void OBSRecordingStoppingEventHandler(object sender, OBSRecordingEventArgs e);

        public event OBSRecordingStoppedEventHandler OnOBSRecordingStopped;
        public delegate void OBSRecordingStoppedEventHandler(object sender, OBSRecordingEventArgs e);

        public event OBSRecordingPausedEventHandler OnOBSRecordingPaused;
        public delegate void OBSRecordingPausedEventHandler(object sender, OBSRecordingEventArgs e);

        public event OBSRecordingResumedEventHandler OnOBSRecordingResumed;
        public delegate void OBSRecordingResumedEventHandler(object sender, OBSRecordingEventArgs e);

        public event OBSReplayStartingEventHandler OnOBSReplayStarting;
        public delegate void OBSReplayStartingEventHandler(object sender, OBSReplayEventArgs e);
        public class OBSReplayEventArgs : EventArgs
        {
        }

        public event OBSReplayStartedEventHandler OnOBSReplayStarted;
        public delegate void OBSReplayStartedEventHandler(object sender, OBSReplayEventArgs e);

        public event OBSReplayStoppingEventHandler OnOBSReplayStopping;
        public delegate void OBSReplayStoppingEventHandler(object sender, OBSReplayEventArgs e);

        public event OBSReplayStoppedEventHandler OnOBSReplayStopped;
        public delegate void OBSReplayStoppedEventHandler(object sender, OBSReplayEventArgs e);

        public event OBSSourceVolumeChangedEventHandler OnOBSSourceVolumeChanged;
        public delegate void OBSSourceVolumeChangedEventHandler(object sender, OBSSourceVolumeChangedEventArgs e);
        public class OBSSourceVolumeChangedEventArgs : EventArgs
        {
            public string sourcename;
            public float sourcevolume;
        }

        public event OBSSourceMuteStateChangedEventHandler OnOBSSourceMuteStateChanged;
        public delegate void OBSSourceMuteStateChangedEventHandler(object sender, OBSSourceMuteStateChangedEventArgs e);
        public class OBSSourceMuteStateChangedEventArgs : EventArgs
        {
            public string sourcename;
            public bool muted;
        }

        public event OBSSourceSourceRenamedEventHandler OnOBSSourceSourceRenamed;
        public delegate void OBSSourceSourceRenamedEventHandler(object sender, OBSSourceSourceRenamedEventArgs e);
        public class OBSSourceSourceRenamedEventArgs : EventArgs
        {
            public string previousName, newName;
        }

        protected virtual void RaiseEvent(JObject message)
        {
            switch ((string)message["update-type"])
            {
                case "SwitchScenes":
                    OnOBSSceneSwitched?.Invoke(this, new OBSSceneSwitchedEventArgs()
                    {
                        newSceneName = (string)message["scene-name"]
                    });
                    break;
                case "StreamStarting":
                    OnOBSStreamStarting?.Invoke(this, new OBSStreamEventArgs());
                    break;
                case "StreamStarted":
                    OnOBSStreamStarted?.Invoke(this, new OBSStreamEventArgs());
                    break;
                case "StreamStopping":
                    OnOBSStreamStopping?.Invoke(this, new OBSStreamEventArgs());
                    break;
                case "StreamStopped":
                    OnOBSStreamStopped?.Invoke(this, new OBSStreamEventArgs());
                    break;
                case "RecordingStarting":
                    OnOBSRecordingStarting?.Invoke(this, new OBSRecordingEventArgs());
                    break;
                case "RecordingStarted":
                    OnOBSRecordingStarted?.Invoke(this, new OBSRecordingEventArgs());
                    break;
                case "RecordingStopping":
                    OnOBSRecordingStopping?.Invoke(this, new OBSRecordingEventArgs());
                    break;
                case "RecordingStopped":
                    OnOBSRecordingStopped?.Invoke(this, new OBSRecordingEventArgs());
                    break;
                case "RecordingPaused":
                    OnOBSRecordingPaused?.Invoke(this, new OBSRecordingEventArgs());
                    break;
                case "RecordingResumed":
                    OnOBSRecordingResumed?.Invoke(this, new OBSRecordingEventArgs());
                    break;
                case "ReplayStarting":
                    OnOBSReplayStarting?.Invoke(this, new OBSReplayEventArgs());
                    break;
                case "ReplayStarted":
                    OnOBSReplayStarted?.Invoke(this, new OBSReplayEventArgs());
                    break;
                case "ReplayStopping":
                    OnOBSReplayStopping?.Invoke(this, new OBSReplayEventArgs());
                    break;
                case "ReplayStopped":
                    OnOBSReplayStopped?.Invoke(this, new OBSReplayEventArgs());
                    break;
                case "SourceVolumeChanged":
                    OnOBSSourceVolumeChanged?.Invoke(this, new OBSSourceVolumeChangedEventArgs()
                    {
                        sourcename = (string)message["sourceName"],
                        sourcevolume = Convert.ToSingle((string)message["volume"])
                    });
                    break;
                case "SourceMuteStateChanged":
                    OnOBSSourceMuteStateChanged?.Invoke(this, new OBSSourceMuteStateChangedEventArgs()
                    {
                        sourcename = (string)message["sourceName"],
                        muted = Convert.ToBoolean((string)message["muted"])
                    });
                    break;
                case "SourceSourceRenamed":
                    OnOBSSourceSourceRenamed?.Invoke(this, new OBSSourceSourceRenamedEventArgs()
                    {
                        previousName = (string)message["previousName"],
                        newName = (string)message["newName"]
                    });
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Event not implemented: {0}", (string)message["update-type"]);
                    break;
            }
        }
    }
}
