using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace OBSWebsocketSharp
{
    public partial class OBSConnector
    {

        public class OBSMessageEventArgs : EventArgs
        {
            public JObject message;
        }
        public delegate void OBSMessageEventHandler(object sender, OBSMessageEventArgs e);
        public event OBSMessageEventHandler OnOBSMessageReceived;

        public class OBSWebsocketEventArgs : EventArgs
        {
            public string text;
        }
        public delegate void OBSWebsocketEventHandler(object sender, OBSWebsocketEventArgs e);
        public event OBSWebsocketEventHandler OnOBSWebsocketInfo, OnOBSWebsocketWarning;

        #region Events from https://github.com/Palakis/obs-websocket/blob/4.x-current/docs/generated/protocol.md
        public class OBSEventArgs : EventArgs { }
        public delegate void OBSEventHandler(object sender, OBSEventArgs e);
        public event OBSEventHandler OnScenesChanged,
            OnSceneCollectionChanged,
            OnSceneCollectionListChanged,
            OnTransitionListChanged,
            OnProfileChanged,
            OnProfileListChanged,
            OnExiting,
            OnStreamStarting,
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

        public event SwitchTransitionEventHandler OnSwitchTransition;
        public delegate void SwitchTransitionEventHandler(object sender, SwitchTransitionEventArgs e);
        public class SwitchTransitionEventArgs : EventArgs
        {
            public string transitionName;
        }

        public event TransitionDurationChangedEventHandler OnTransitionDurationChanged;
        public delegate void TransitionDurationChangedEventHandler(object sender, TransitionDurationChangedEventArgs e);
        public class TransitionDurationChangedEventArgs : EventArgs
        {
            public int newDuration;
        }

        public event TransitionBeginEventHandler OnTransitionBegin;
        public delegate void TransitionBeginEventHandler(object sender, TransitionBeginEventArgs e);
        public class TransitionBeginEventArgs : EventArgs
        {
            public string name, type, fromScene, toScene;
            public int duration;
        }

        public event TransitionEndEventHandler OnTransitionEnd;
        public delegate void TransitionEndEventHandler(object sender, TransitionEndEventArgs e);
        public class TransitionEndEventArgs : EventArgs
        {
            public string name, type, toScene;
            public int duration;
        }

        public event TransitionVideoEndEventHandler OnTransitionVideoEnd;
        public delegate void TransitionVideoEndEventHandler(object sender, TransitionVideoEndEventArgs e);
        public class TransitionVideoEndEventArgs : EventArgs
        {
            public string name, type, fromScene, toScene;
            public int duration;
        }

        public event StreamStatusUpdateEventHandler OnStreamStatusUpdate;
        public delegate void StreamStatusUpdateEventHandler(object sender, StreamStatusUpdateEventArgs e);
        public class StreamStatusUpdateEventArgs : EventArgs
        {
            public bool streaming, recording, recordingPaused, replayBufferActive;
            public int bytesPerSec, kbitsPerSec, totalStreamTime, numTotalFrames, numDroppedFrames, renderTotalFrames, renderMissedFrames, outputTotalFrames, outputSkippedFrames;
            public double strain, fps, averageFrameTime, cpuUsage, memoryUsage, freeDiskSpace;
        }

        public event HeartbeatEventHandler OnHeartbeat;
        public delegate void HeartbeatEventHandler(object sender, HeartbeatEventArgs e);
        public class HeartbeatEventArgs : EventArgs
        {
            public bool pulse, streaming, recording;
            public string currentProfile, currentScene;
            public int totalStreamTime, totalStreamBytes, totalStreamFrames, totalRecordTime, totalRecordBytes, totalRecordFrames;
            public OBSStats stats;
        }

        public event SourceEventHandler OnSourceCreated,
            OnSourceDestroyed;
        public delegate void SourceEventHandler(object sender, SourceEventArgs e);
        public class SourceEventArgs : EventArgs //no sourceSettings
        {
            public string sourceName, sourceType, sourceKind;
        }

        public event SourceAudioSyncOffsetChangedHandler OnSourceAudioSyncOffsetChanged;
        public delegate void SourceAudioSyncOffsetChangedHandler(object sender, SourceAudioSyncOffsetChangedArgs e);
        public class SourceAudioSyncOffsetChangedArgs : EventArgs
        {
            public string sourceName;
            public int syncOffset;
        }

        public event SceneSwitchedEventHandler OnSceneSwitched;
        public delegate void SceneSwitchedEventHandler(object sender, SceneSwitchedEventArgs e);
        public class SceneSwitchedEventArgs : EventArgs
        {
            public string newSceneName;
            public SceneItem[] sources; 
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
            public string sourceName;
            public bool muted;
        }

        public event SourceAudioMixersChangedHandler OnSourceAudioMixersChanged;
        public delegate void SourceAudioMixersChangedHandler(object sender, SourceAudioMixersChangedArgs e);
        public class SourceAudioMixersChangedArgs : EventArgs
        {
            public string sourceName, hexMixersValue;
            public Mixer[] mixers;
        }

        public event SourceRenamedEventHandler OnSourceRenamed;
        public delegate void SourceRenamedEventHandler(object sender, SourceRenamedEventArgs e);
        public class SourceRenamedEventArgs : EventArgs
        {
            public string previousName, newName;
        }

        public event SourceFilterEventHandler OnSourceFilterAdded,
            OnSourceFilterRemoved;
        public delegate void SourceFilterEventHandler(object sender, SourceFilterEventArgs e);
        public class SourceFilterEventArgs : EventArgs //No filterSettings
        {
            public string sourceName, filterName, filterType;
        }

        public event SourceFilterVisibilityChangedEventHandler OnSourceFilterVisibilityChanged;
        public delegate void SourceFilterVisibilityChangedEventHandler(object sender, SourceFilterVisibilityChangedEventArgs e);
        public class SourceFilterVisibilityChangedEventArgs : EventArgs //No filterSettings
        {
            public string sourceName, filterName;
            public bool filterEnabled;
        }

        public event SourceFiltersReorderedEventHandler OnSourceFiltersReordered;
        public delegate void SourceFiltersReorderedEventHandler(object sender, SourceFiltersReorderedEventArgs e);
        public class SourceFiltersReorderedEventArgs : EventArgs
        {
            public string sourceName;
            public Filter[] filters;
        }

        public event SceneItemEventHandler OnSceneItemAdded,
            OnSceneItemRemoved;
        public delegate void SceneItemEventHandler(object sender, SceneItemEventArgs e);
        public class SceneItemEventArgs : EventArgs
        {
            public string sceneName, itemName;
            public int itemId;
        }

        public event SceneItemVisibilityChangedEventHandler OnSceneItemVisibilityChanged;
        public delegate void SceneItemVisibilityChangedEventHandler(object sender, SceneItemVisibilityChangedEventArgs e);
        public class SceneItemVisibilityChangedEventArgs : EventArgs
        {
            public string sceneName, itemName;
            public int itemId;
            public bool itemVisible;
        }

        public event SceneItemLockChangedEventHandler OnSceneItemLockChanged;
        public delegate void SceneItemLockChangedEventHandler(object sender, SceneItemLockChangedEventArgs e);
        public class SceneItemLockChangedEventArgs : EventArgs
        {
            public string sceneName, itemName;
            public int itemId;
            public bool itemLocked;
        }

        public event StudioModeSwitchedEventHandler OnStudioModeSwitched;
        public delegate void StudioModeSwitchedEventHandler(object sender, StudioModeSwitchedEventArgs e);
        public class StudioModeSwitchedEventArgs : EventArgs
        {
            public bool newState;
        }
        #endregion

        protected virtual void RaiseEvent(JObject message)
        {
            switch (message["update-type"].ToObject<string>())
            {
                case "SwitchScenes":
                    List<SceneItem> sources = new List<SceneItem>();
                    foreach(JObject source in message["sources"])
                    {
                        sources.Add(new SceneItem()
                        {
                            alignment = source["alignment"].ToObject<int>(),
                            cx = source["cx"].ToObject<double>(),
                            x = source["x"].ToObject<double>(),
                            cy = source["cy"].ToObject<double>(),
                            y = source["y"].ToObject<double>(),
                            volume = source["volume"].ToObject<double>(),
                            locked = source["locked"].ToObject<bool>(),
                            name = source["name"].ToObject<string>(),
                            type = source["type"].ToObject<string>(),
                            id = source["id"].ToObject<int>(),
                            render = source["render"].ToObject<bool>(),
                            muted = source["muted"].ToObject<bool>(),
                            source_cx = source["source_cx"].ToObject<int>(),
                            source_cy = source["source_cy"].ToObject<int>()
                        });
                    }
                    OnSceneSwitched?.Invoke(this, new SceneSwitchedEventArgs()
                    {
                        newSceneName = message["scene-name"].ToObject<string>(),
                        sources = sources.ToArray()
                    }) ;
                    break;
                case "ScenesChanged":
                    OnScenesChanged?.Invoke(this, new OBSEventArgs());
                    break;
                case "SceneCollectionChanged":
                    OnSceneCollectionChanged?.Invoke(this, new OBSEventArgs());
                    break;
                case "SceneCollectionListChanged":
                    OnSceneCollectionListChanged?.Invoke(this, new OBSEventArgs());
                    break;
                case "SwitchTransition":
                    OnSwitchTransition?.Invoke(this, new SwitchTransitionEventArgs()
                    {
                         transitionName = message["transition-name"].ToObject<string>()
                    });
                    break;
                case "TransitionListChanged":
                    OnTransitionListChanged?.Invoke(this, new OBSEventArgs());
                    break;
                case "TransitionDurationChanged":
                    OnTransitionDurationChanged?.Invoke(this, new TransitionDurationChangedEventArgs()
                    {
                        newDuration = message["new-duration"].ToObject<int>()
                    });
                    break;
                case "TransitionBegin":
                    OnTransitionBegin?.Invoke(this, new TransitionBeginEventArgs()
                    {
                        duration = message["duration"].ToObject<int>(),
                        fromScene = message["from-scene"].ToObject<string>(),
                        toScene = message["to-scene"].ToObject<string>(),
                        name = message["name"].ToObject<string>(),
                        type = message["type"].ToObject<string>()
                    });
                    break;
                case "TransitionEnd":
                    OnTransitionEnd?.Invoke(this, new TransitionEndEventArgs()
                    {
                        duration = message["duration"].ToObject<int>(),
                        toScene = message["to-scene"].ToObject<string>(),
                        name = message["name"].ToObject<string>(),
                        type = message["type"].ToObject<string>()
                    });
                    break;
                case "TransitionVideoEnd":
                    OnTransitionVideoEnd?.Invoke(this, new TransitionVideoEndEventArgs()
                    {
                        duration = message["duration"].ToObject<int>(),
                        fromScene = message["from-scene"].ToObject<string>(),
                        toScene = message["to-scene"].ToObject<string>(),
                        name = message["name"].ToObject<string>(),
                        type = message["type"].ToObject<string>()
                    });
                    break;
                case "ProfileChanged":
                    OnProfileChanged?.Invoke(this, new OBSEventArgs());
                    break;
                case "ProfileListChanged":
                    OnProfileListChanged?.Invoke(this, new OBSEventArgs());
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
                case "StreamStatus":
                    OnStreamStatusUpdate?.Invoke(this, new StreamStatusUpdateEventArgs()
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
                    });
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
                case "Exiting":
                    OnExiting?.Invoke(this, new OBSEventArgs());
                    break;
                case "Heartbeat":
                    OnHeartbeat?.Invoke(this, new HeartbeatEventArgs()
                    {
                        pulse = message["pulse"].ToObject<bool>(),
                        streaming = message.ContainsKey("streaming") ? message["streaming"].ToObject<bool>() : false,
                        recording = message.ContainsKey("recording") ? message["recording"].ToObject<bool>() : false,
                        currentProfile = message.ContainsKey("current-profile") ? message["current-profile"].ToObject<string>() : string.Empty,
                        currentScene = message.ContainsKey("current-scene") ? message["current-scene"].ToObject<string>() : string.Empty,
                        totalStreamTime = message.ContainsKey("total-stream-time") ? message["total-stream-time"].ToObject<int>() : 0,
                        totalStreamBytes = message.ContainsKey("total-stream-bytes") ? message["total-stream-bytes"].ToObject<int>() : 0,
                        totalStreamFrames = message.ContainsKey("total-stream-frames") ? message["total-stream-frames"].ToObject<int>() : 0,
                        totalRecordTime = message.ContainsKey("total-record-time") ? message["total-record-time"].ToObject<int>() : 0,
                        totalRecordBytes = message.ContainsKey("total-record-bytes") ? message["total-record-bytes"].ToObject<int>() : 0,
                        totalRecordFrames = message.ContainsKey("total-record-frames") ? message["total-record-frames"].ToObject<int>() : 0,
                        stats = message.ContainsKey("stats") ? (new OBSStats()
                        {
                            fps = message["stats"]["fps"].ToObject<double>(),
                            averageframetime = message["stats"]["average-frame-time"].ToObject<double>(),
                            cpuusage = message["stats"]["cpu-usage"].ToObject<double>(),
                            memoryusage = message["stats"]["memory-usage"].ToObject<double>(),
                            freediskspace = message["stats"]["free-disk-space"].ToObject<double>(),
                            rendertotalframes = message["stats"]["render-total-frames"].ToObject<int>(),
                            rendermissedframes = message["stats"]["render-missed-frames"].ToObject<int>(),
                            outputtotalframes = message["stats"]["output-total-frames"].ToObject<int>(),
                            outputskippedframes = message["stats"]["output-skipped-frames"].ToObject<int>()
                        }) : new OBSStats()
                    });
                    break;
                case "SourceCreated":
                    OnSourceCreated?.Invoke(this, new SourceEventArgs()
                    {
                        sourceName = message["sourceName"].ToObject<string>(),
                        sourceType = message["sourceType"].ToObject<string>(),
                        sourceKind = message["sourceKind"].ToObject<string>()
                    });
                    break;
                case "SourceDestroyed":
                    OnSourceDestroyed?.Invoke(this, new SourceEventArgs()
                    {
                        sourceName = message["sourceName"].ToObject<string>(),
                        sourceType = message["sourceType"].ToObject<string>(),
                        sourceKind = message["sourceKind"].ToObject<string>()
                    });
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
                        sourceName = message["sourceName"].ToObject<string>(),
                        muted = message["muted"].ToObject<bool>()
                    });
                    break;
                case "SourceAudioSyncOffsetChanged":
                    OnSourceAudioSyncOffsetChanged?.Invoke(this, new SourceAudioSyncOffsetChangedArgs()
                    {
                        sourceName = message["sourceName"].ToObject<string>(),
                        syncOffset = message["syncOffset"].ToObject<int>()
                    });
                    break;
                case "SourceAudioMixersChanged":
                    List<Mixer> mixerlist = new List<Mixer>();
                    foreach (JObject mixer in message["mixers"])
                        mixerlist.Add(new Mixer()
                        {
                            id = mixer["id"].ToObject<int>(),
                            enabled = mixer["enabled"].ToObject<bool>()
                        });
                    OnSourceAudioMixersChanged?.Invoke(this, new SourceAudioMixersChangedArgs()
                    {
                        sourceName = message["sourceName"].ToObject<string>(),
                        hexMixersValue = message["hexMixersValue"].ToObject<string>(),
                        mixers = mixerlist.ToArray()
                    });
                    break;
                case "SourceRenamed":
                    OnSourceRenamed?.Invoke(this, new SourceRenamedEventArgs()
                    {
                        previousName = message["previousName"].ToObject<string>(),
                        newName = message["newName"].ToObject<string>()
                    });
                    break;
                case "SourceFilterAdded":
                    OnSourceFilterAdded?.Invoke(this, new SourceFilterEventArgs()
                    {
                        filterName = message["filterName"].ToObject<string>(),
                        sourceName = message["sourceName"].ToObject<string>(),
                        filterType = message["filterType"].ToObject<string>()
                    });
                    break;
                case "SourceFilterRemoved":
                    OnSourceFilterAdded?.Invoke(this, new SourceFilterEventArgs()
                    {
                        filterName = message["filterName"].ToObject<string>(),
                        sourceName = message["sourceName"].ToObject<string>(),
                        filterType = message["filterType"].ToObject<string>()
                    });
                    break;
                case "SourceFilterVisibilityChanged":
                    OnSourceFilterVisibilityChanged?.Invoke(this, new SourceFilterVisibilityChangedEventArgs()
                    {
                        filterName = message["filterName"].ToObject<string>(),
                        sourceName = message["sourceName"].ToObject<string>(),
                        filterEnabled = message["filterEnabled"].ToObject<bool>()
                    });
                    break;
                case "SourceFiltersReordered":
                    List<Filter> filterlist = new List<Filter>();
                    foreach (JObject filter in message["filters"])
                        filterlist.Add(new Filter()
                        {
                            name = filter["name"].ToObject<string>(),
                            type = filter["type"].ToObject<string>()
                        });
                    OnSourceFiltersReordered?.Invoke(this, new SourceFiltersReorderedEventArgs()
                    {
                        sourceName = message["sourceName"].ToObject<string>(),
                        filters = filterlist.ToArray()
                    });
                    break;
                case "SceneItemAdded":
                    OnSceneItemAdded?.Invoke(this, new SceneItemEventArgs()
                    {
                        sceneName = message["scene-name"].ToObject<string>(),
                        itemName = message["item-name"].ToObject<string>(),
                        itemId = message["item-id"].ToObject<int>()
                    });
                    break;
                case "SceneItemRemoved":
                    OnSceneItemRemoved?.Invoke(this, new SceneItemEventArgs()
                    {
                        sceneName = message["scene-name"].ToObject<string>(),
                        itemName = message["item-name"].ToObject<string>(),
                        itemId = message["item-id"].ToObject<int>()
                    });
                    break;
                case "SceneItemVisibilityChanged":
                    OnSceneItemVisibilityChanged?.Invoke(this, new SceneItemVisibilityChangedEventArgs()
                    {
                        sceneName = message["scene-name"].ToObject<string>(),
                        itemName = message["item-name"].ToObject<string>(),
                        itemId = message["item-id"].ToObject<int>(),
                        itemVisible = message["item-visible"].ToObject<bool>()
                    });
                    break;
                case "SceneItemLockChanged":
                    OnSceneItemLockChanged?.Invoke(this, new SceneItemLockChangedEventArgs()
                    {
                        sceneName = message["scene-name"].ToObject<string>(),
                        itemName = message["item-name"].ToObject<string>(),
                        itemId = message["item-id"].ToObject<int>(),
                        itemLocked = message["item-locked"].ToObject<bool>()
                    });
                    break;
                case "StudioModeSwitched":
                    OnStudioModeSwitched?.Invoke(this, new StudioModeSwitchedEventArgs()
                    {
                        newState = message["new-state"].ToObject<bool>()
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
