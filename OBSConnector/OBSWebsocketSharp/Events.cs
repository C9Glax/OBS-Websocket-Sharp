using Newtonsoft.Json.Linq;
using System;

namespace OBSWebsocketSharp
{
    public partial class OBSConnector
    {
        public class OBSWebsocketEventArgs : EventArgs
        {
            public string text;
        }
        public delegate void OBSWebsocketEventHandler(object sender, OBSWebsocketEventArgs e);
        public event OBSWebsocketEventHandler OnOBSWebsocketInfo, OnOBSWebsocketWarning;

        public class OBSEventArgs : EventArgs { }
        public delegate void OBSEventHandler(object sender, OBSEventArgs e);
        public event OBSEventHandler OnStreamStarting,
            OnStreamStarted,
            OnStreamStopping,
            OnStreamStopped,
            OnRecordingStarting,
            OnRecordingStarted,
            OnRecordingStopping,
            OnRecordingStopped,
            OnRecordingPaused,
            OnRecordingResumed,
            OnReplayStarting,
            OnReplayStarted,
            OnReplayStopping,
            OnReplayStopped;

        public event SceneSwitchedEventHandler OnSceneSwitched;
        public delegate void SceneSwitchedEventHandler(object sender, SceneSwitchedEventArgs e);
        public class SceneSwitchedEventArgs : EventArgs
        {
            public string newSceneName;
        }

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
            switch (message["update-type"].ToObject<string>())
            {
                case "SwitchScenes":
                    OnSceneSwitched?.Invoke(this, new SceneSwitchedEventArgs()
                    {
                        newSceneName = message["scene-name"].ToObject<string>()
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
                        sourcename = message["sourceName"].ToObject<string>(),
                        sourcevolume = Convert.ToSingle(message["volume"].ToObject<string>())
                    });
                    break;
                case "SourceMuteStateChanged":
                    OnSourceMuteChanged?.Invoke(this, new SourceMuteChangedEventArgs()
                    {
                        sourcename = message["sourceName"].ToObject<string>(),
                        muted = message["muted"].ToObject<bool>()
                    });
                    break;
                case "SourceSourceRenamed":
                    OnSourceRenamed?.Invoke(this, new SourceRenamedEventArgs()
                    {
                        previousName = message["previousName"].ToObject<string>(),
                        newName = message["newName"].ToObject<string>()
                    });
                    break;
                case "StreamStatus":
                    OnStreamStatusUpdate?.Invoke(this, new StreamStatusUpdateEventArgs()
                    {
                        status = new StreamStatus()
                        {
                            recording = message["recording"].ToObject<bool>(),
                            recordingPaused = message["recording-paused"].ToObject<bool>(),
                            replayBufferActive = message["replay-buffer-active"].ToObject<bool>(),
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
                    this.OnOBSWebsocketInfo?.Invoke(this, new OBSWebsocketEventArgs()
                    {
                        text = string.Format("Event not implemented: {0}", (string)message["update-type"])
                    });
                    break;
            }
        }
    }
}
