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
        public delegate void OBSStreamStartingEventHandler(object sender, OBSStreamStartingEventArgs e);
        public class OBSStreamStartingEventArgs : EventArgs
        {
            public bool previewOnly = false;
        }

        public event OBSStreamStartedEventHandler OnOBSStreamStarted;
        public delegate void OBSStreamStartedEventHandler(object sender, OBSStreamStartedEventArgs e);
        public class OBSStreamStartedEventArgs : EventArgs
        {
        }

        public event OBSStreamStoppingEventHandler OnOBSStreamStopping;
        public delegate void OBSStreamStoppingEventHandler(object sender, OBSStreamStoppingEventArgs e);
        public class OBSStreamStoppingEventArgs : EventArgs
        {
            public bool previewOnly = false;
        }

        public event OBSStreamStoppedEventHandler OnOBSStreamStopped;
        public delegate void OBSStreamStoppedEventHandler(object sender, OBSStreamStoppedEventArgs e);
        public class OBSStreamStoppedEventArgs : EventArgs
        {
        }

        public event OBSRecordingStartingEventHandler OnOBSRecordingStarting;
        public delegate void OBSRecordingStartingEventHandler(object sender, OBSRecordingStartingEventArgs e);
        public class OBSRecordingStartingEventArgs : EventArgs
        {
            public bool previewOnly = false;
        }

        public event OBSRecordingStartedEventHandler OnOBSRecordingStarted;
        public delegate void OBSRecordingStartedEventHandler(object sender, OBSRecordingStartedEventArgs e);
        public class OBSRecordingStartedEventArgs : EventArgs
        {
        }

        public event OBSRecordingStoppingEventHandler OnOBSRecordingStopping;
        public delegate void OBSRecordingStoppingEventHandler(object sender, OBSRecordingStoppingEventArgs e);
        public class OBSRecordingStoppingEventArgs : EventArgs
        {
            public bool previewOnly = false;
        }

        public event OBSRecordingStoppedEventHandler OnOBSRecordingStopped;
        public delegate void OBSRecordingStoppedEventHandler(object sender, OBSRecordingStoppedEventArgs e);
        public class OBSRecordingStoppedEventArgs : EventArgs
        {
        }

        public event OBSRecordingPausedEventHandler OnOBSRecordingPaused;
        public delegate void OBSRecordingPausedEventHandler(object sender, OBSRecordingPausedEventArgs e);
        public class OBSRecordingPausedEventArgs : EventArgs
        {
            public bool previewOnly = false;
        }

        public event OBSRecordingResumedEventHandler OnOBSRecordingResumed;
        public delegate void OBSRecordingResumedEventHandler(object sender, OBSRecordingResumedEventArgs e);
        public class OBSRecordingResumedEventArgs : EventArgs
        {
        }

        public event OBSReplayStartingEventHandler OnOBSReplayStarting;
        public delegate void OBSReplayStartingEventHandler(object sender, OBSReplayStartingEventArgs e);
        public class OBSReplayStartingEventArgs : EventArgs
        {
            public bool previewOnly = false;
        }

        public event OBSReplayStartedEventHandler OnOBSReplayStarted;
        public delegate void OBSReplayStartedEventHandler(object sender, OBSReplayStartedEventArgs e);
        public class OBSReplayStartedEventArgs : EventArgs
        {
        }

        public event OBSReplayStoppingEventHandler OnOBSReplayStopping;
        public delegate void OBSReplayStoppingEventHandler(object sender, OBSReplayStoppingEventArgs e);
        public class OBSReplayStoppingEventArgs : EventArgs
        {
            public bool previewOnly = false;
        }

        public event OBSReplayStoppedEventHandler OnOBSReplayStopped;
        public delegate void OBSReplayStoppedEventHandler(object sender, OBSReplayStoppedEventArgs e);
        public class OBSReplayStoppedEventArgs : EventArgs
        {
        }

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
                    OnOBSStreamStarting?.Invoke(this, new OBSStreamStartingEventArgs());
                    break;
                case "StreamStarted":
                    OnOBSStreamStarted?.Invoke(this, new OBSStreamStartedEventArgs());
                    break;
                case "StreamStopping":
                    OnOBSStreamStopping?.Invoke(this, new OBSStreamStoppingEventArgs());
                    break;
                case "StreamStopped":
                    OnOBSStreamStopped?.Invoke(this, new OBSStreamStoppedEventArgs());
                    break;
                case "RecordingStarting":
                    OnOBSRecordingStarting?.Invoke(this, new OBSRecordingStartingEventArgs());
                    break;
                case "RecordingStarted":
                    OnOBSRecordingStarted?.Invoke(this, new OBSRecordingStartedEventArgs());
                    break;
                case "RecordingStopping":
                    OnOBSRecordingStopping?.Invoke(this, new OBSRecordingStoppingEventArgs());
                    break;
                case "RecordingStopped":
                    OnOBSRecordingStopped?.Invoke(this, new OBSRecordingStoppedEventArgs());
                    break;
                case "RecordingPaused":
                    OnOBSRecordingPaused?.Invoke(this, new OBSRecordingPausedEventArgs());
                    break;
                case "RecordingResumed":
                    OnOBSRecordingResumed?.Invoke(this, new OBSRecordingResumedEventArgs());
                    break;
                case "ReplayStarting":
                    OnOBSReplayStarting?.Invoke(this, new OBSReplayStartingEventArgs());
                    break;
                case "ReplayStarted":
                    OnOBSReplayStarted?.Invoke(this, new OBSReplayStartedEventArgs());
                    break;
                case "ReplayStopping":
                    OnOBSReplayStopping?.Invoke(this, new OBSReplayStoppingEventArgs());
                    break;
                case "ReplayStopped":
                    OnOBSReplayStopped?.Invoke(this, new OBSReplayStoppedEventArgs());
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
