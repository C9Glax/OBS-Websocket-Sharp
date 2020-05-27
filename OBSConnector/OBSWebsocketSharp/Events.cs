using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace OBSWebsocketSharp
{
    public partial class OBSConnector
    {

        public event SceneSwitchedEventHandler OnSceneSwitched;
        public delegate void SceneSwitchedEventHandler(object sender, SceneSwitchedEventArgs e);
        public class SceneSwitchedEventArgs : EventArgs
        {
            public string newSceneName;
        }
        public class OBSEventArgs : EventArgs
        {
        }

        public event StreamStartingEventHandler OnStreamStarting;
        public delegate void StreamStartingEventHandler(object sender, OBSEventArgs e);

        public event StreamStartedEventHandler OnStreamStarted;
        public delegate void StreamStartedEventHandler(object sender, OBSEventArgs e);

        public event StreamStoppingEventHandler OnStreamStopping;
        public delegate void StreamStoppingEventHandler(object sender, OBSEventArgs e);

        public event StreamStoppedEventHandler OnStreamStopped;
        public delegate void StreamStoppedEventHandler(object sender, OBSEventArgs e);

        public event RecordingStartingEventHandler OnRecordingStarting;
        public delegate void RecordingStartingEventHandler(object sender, OBSEventArgs e);

        public event RecordingStartedEventHandler OnRecordingStarted;
        public delegate void RecordingStartedEventHandler(object sender, OBSEventArgs e);

        public event RecordingStoppingEventHandler OnRecordingStopping;
        public delegate void RecordingStoppingEventHandler(object sender, OBSEventArgs e);

        public event RecordingStoppedEventHandler OnRecordingStopped;
        public delegate void RecordingStoppedEventHandler(object sender, OBSEventArgs e);

        public event RecordingPausedEventHandler OnRecordingPaused;
        public delegate void RecordingPausedEventHandler(object sender, OBSEventArgs e);

        public event RecordingResumedEventHandler OnRecordingResumed;
        public delegate void RecordingResumedEventHandler(object sender, OBSEventArgs e);

        public event ReplayStartingEventHandler OnReplayStarting;
        public delegate void ReplayStartingEventHandler(object sender, OBSEventArgs e);

        public event ReplayStartedEventHandler OnReplayStarted;
        public delegate void ReplayStartedEventHandler(object sender, OBSEventArgs e);

        public event ReplayStoppingEventHandler OnReplayStopping;
        public delegate void ReplayStoppingEventHandler(object sender, OBSEventArgs e);

        public event ReplayStoppedEventHandler OnReplayStopped;
        public delegate void ReplayStoppedEventHandler(object sender, OBSEventArgs e);

        public event SourceVolumeChangedEventHandler OnSourceVolumeChanged;
        public delegate void SourceVolumeChangedEventHandler(object sender, SourceVolumeChangedEventArgs e);
        public class SourceVolumeChangedEventArgs : EventArgs
        {
            public string sourcename;
            public float sourcevolume;
        }

        public event SourceMuteChangedEventHandler OnSourceMuteChanged;
        public delegate void SourceMuteChangedEventHandler(object sender, SourceMuteChangedEventArgs e);
        public class SourceMuteChangedEventArgs : EventArgs
        {
            public string sourcename;
            public bool muted;
        }

        public event SourceRenamedEventHandler OnSourceRenamed;
        public delegate void SourceRenamedEventHandler(object sender, SourceRenamedEventArgs e);
        public class SourceRenamedEventArgs : EventArgs
        {
            public string previousName, newName;
        }

        public event StreamStatusUpdateEventHandler OnStreamStatusUpdate;
        public delegate void StreamStatusUpdateEventHandler(object sender, StreamStatusUpdateEventArgs e);
        public class StreamStatusUpdateEventArgs : EventArgs
        {
            public StreamStatus status;
        }

        protected virtual void RaiseEvent(JObject message)
        {
            switch ((string)message["update-type"])
            {
                case "SwitchScenes":
                    OnSceneSwitched?.Invoke(this, new SceneSwitchedEventArgs()
                    {
                        newSceneName = (string)message["scene-name"]
                    });
                    break;
                case "StreamStarting":
                    OnStreamStarting?.Invoke(this, new OBSEventArgs());
                    break;
                case "StreamStarted":
                    OnStreamStarted?.Invoke(this, new OBSEventArgs());
                    break;
                case "StreamStopping":
                    OnStreamStopping?.Invoke(this, new OBSEventArgs());
                    break;
                case "StreamStopped":
                    OnStreamStopped?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingStarting":
                    OnRecordingStarting?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingStarted":
                    OnRecordingStarted?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingStopping":
                    OnRecordingStopping?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingStopped":
                    OnRecordingStopped?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingPaused":
                    OnRecordingPaused?.Invoke(this, new OBSEventArgs());
                    break;
                case "RecordingResumed":
                    OnRecordingResumed?.Invoke(this, new OBSEventArgs());
                    break;
                case "ReplayStarting":
                    OnReplayStarting?.Invoke(this, new OBSEventArgs());
                    break;
                case "ReplayStarted":
                    OnReplayStarted?.Invoke(this, new OBSEventArgs());
                    break;
                case "ReplayStopping":
                    OnReplayStopping?.Invoke(this, new OBSEventArgs());
                    break;
                case "ReplayStopped":
                    OnReplayStopped?.Invoke(this, new OBSEventArgs());
                    break;
                case "SourceVolumeChanged":
                    OnSourceVolumeChanged?.Invoke(this, new SourceVolumeChangedEventArgs()
                    {
                        sourcename = (string)message["sourceName"],
                        sourcevolume = Convert.ToSingle((string)message["volume"])
                    });
                    break;
                case "SourceMuteStateChanged":
                    OnSourceMuteChanged?.Invoke(this, new SourceMuteChangedEventArgs()
                    {
                        sourcename = (string)message["sourceName"],
                        muted = Convert.ToBoolean((string)message["muted"])
                    });
                    break;
                case "SourceSourceRenamed":
                    OnSourceRenamed?.Invoke(this, new SourceRenamedEventArgs()
                    {
                        previousName = (string)message["previousName"],
                        newName = (string)message["newName"]
                    });
                    break;
                case "StreamStatus":
                    OnStreamStatusUpdate?.Invoke(this, new StreamStatusUpdateEventArgs()
                    {
                        status = new StreamStatus()
                        {
                            recording = message["recording"].ToObject<bool>(),
                            replayBufferActive = message["replay-buffer-active	"].ToObject<bool>(),
                            streaming = message["streaming"].ToObject<bool>(),
                            bytesPerSec = message["bytes-per-sec"].ToObject<int>(),
                            kbitsPerSec = message["kbits-per-sec"].ToObject<int>(),
                            totalStreamTime = message["total-stream-time"].ToObject<int>(),
                            numTotalFrames = message["num-total-frames"].ToObject<int>(),
                            numDroppedFrames = message["num-dropped-frames"].ToObject<int>(),
                            renderTotalFrames = message["render-total-frames"].ToObject<int>(),
                            renderMissedFrames = message["render-missed-frames"].ToObject<int>(),
                            outputTotalFrames = message["output-total-frames"].ToObject<int>(),
                            outputSkippedFrames = message["output-skipped-frames"].ToObject<int>(),
                            strain = message["strain"].ToObject<double>(),
                            fps = message["fps"].ToObject<double>(),
                            averageFrameTime = message["average-frame-time"].ToObject<double>(),
                            cpuUsage = message["cpu-usage"].ToObject<double>(),
                            memoryUsage = message["memory-usage"].ToObject<double>(),
                            freeDiskSpace = message["free-disk-space"].ToObject<double>()
                        }
                    });
                    break;
                default:
                    Console.WriteLine("Event not implemented: {0}", (string)message["update-type"]);
                    break;
            }
        }
    }
}
