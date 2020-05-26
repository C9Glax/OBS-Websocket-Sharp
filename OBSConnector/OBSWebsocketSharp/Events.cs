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
        public class OBSEventArgs : EventArgs
        {
        }

        public event OBSStreamStartingEventHandler OnOBSStreamStarting;
        public delegate void OBSStreamStartingEventHandler(object sender, OBSEventArgs e);

        public event OBSStreamStartedEventHandler OnOBSStreamStarted;
        public delegate void OBSStreamStartedEventHandler(object sender, OBSEventArgs e);

        public event OBSStreamStoppingEventHandler OnOBSStreamStopping;
        public delegate void OBSStreamStoppingEventHandler(object sender, OBSEventArgs e);

        public event OBSStreamStoppedEventHandler OnOBSStreamStopped;
        public delegate void OBSStreamStoppedEventHandler(object sender, OBSEventArgs e);

        public event OBSRecordingStartingEventHandler OnOBSRecordingStarting;
        public delegate void OBSRecordingStartingEventHandler(object sender, OBSEventArgs e);

        public event OBSRecordingStartedEventHandler OnOBSRecordingStarted;
        public delegate void OBSRecordingStartedEventHandler(object sender, OBSEventArgs e);

        public event OBSRecordingStoppingEventHandler OnOBSRecordingStopping;
        public delegate void OBSRecordingStoppingEventHandler(object sender, OBSEventArgs e);

        public event OBSRecordingStoppedEventHandler OnOBSRecordingStopped;
        public delegate void OBSRecordingStoppedEventHandler(object sender, OBSEventArgs e);

        public event OBSRecordingPausedEventHandler OnOBSRecordingPaused;
        public delegate void OBSRecordingPausedEventHandler(object sender, OBSEventArgs e);

        public event OBSRecordingResumedEventHandler OnOBSRecordingResumed;
        public delegate void OBSRecordingResumedEventHandler(object sender, OBSEventArgs e);

        public event OBSReplayStartingEventHandler OnOBSReplayStarting;
        public delegate void OBSReplayStartingEventHandler(object sender, OBSEventArgs e);

        public event OBSReplayStartedEventHandler OnOBSReplayStarted;
        public delegate void OBSReplayStartedEventHandler(object sender, OBSEventArgs e);

        public event OBSReplayStoppingEventHandler OnOBSReplayStopping;
        public delegate void OBSReplayStoppingEventHandler(object sender, OBSEventArgs e);

        public event OBSReplayStoppedEventHandler OnOBSReplayStopped;
        public delegate void OBSReplayStoppedEventHandler(object sender, OBSEventArgs e);

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
                    OnOBSStreamStarting?.Invoke(this, new OBSEventArgs());
                    break;
                case "StreamStarted":
                    OnOBSStreamStarted?.Invoke(this, new OBSEventArgs());
                    break;
                case "StreamStopping":
                    OnOBSStreamStopping?.Invoke(this, new OBSEventArgs());
                    break;
                case "StreamStopped":
                    OnOBSStreamStopped?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingStarting":
                    OnOBSRecordingStarting?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingStarted":
                    OnOBSRecordingStarted?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingStopping":
                    OnOBSRecordingStopping?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingStopped":
                    OnOBSRecordingStopped?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingPaused":
                    OnOBSRecordingPaused?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingResumed":
                    OnOBSRecordingResumed?.Invoke(this, new OBSEventArgs());
                    break;
                case "ReplayStarting":
                    OnOBSReplayStarting?.Invoke(this, new OBSEventArgs());
                    break;
                case "ReplayStarted":
                    OnOBSReplayStarted?.Invoke(this, new OBSEventArgs());
                    break;
                case "ReplayStopping":
                    OnOBSReplayStopping?.Invoke(this, new OBSEventArgs());
                    break;
                case "ReplayStopped":
                    OnOBSReplayStopped?.Invoke(this, new OBSEventArgs());
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
                    Console.WriteLine("Event not implemented: {0}", (string)message["update-type"]);
                    break;
            }
        }
    }
}
