using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace OBSWebsocketSharp
{
    #region Objects from https://github.com/Palakis/obs-websocket/blob/4.x-current/docs/generated/protocol.md
    public struct Scene
    {
        public string name;
        public SceneItem[] sources;
    }

    public struct SceneItem //No groupChildren
    {
        public double cx, cy, x, y, volume;
        public int alignment, id, source_cx, source_cy;
        public string name, type;
        public bool render, muted, locked;
    }

    public struct SceneItemTransformChangedObject
    {
        public string sceneName, itemName;
        public int itemId;
        public SceneItemTransform transform;
    }

    public struct SceneItemTransform
    {
        public Bounds bounds;
        public Crop crop;
        public Position position;
        public Scale scale;
        public double rotation, height, width;
        public bool visible, locked;
        public int sourceWidth, sourceHeight;
        public SceneItemTransform[] groupChildren;
        public string parentGroupName;

        public struct Bounds
        {
            public int alignment;
            public string type;
            public double x, y;
        }

        public struct Crop
        {
            public int top, bottom, right, left;
        }

        public struct Position
        {
            public int alignment;
            public double x, y;
        }

        public struct Scale
        {
            public double x, y;
        }
    }

    public struct Output
    {
        public string name, type;
        public int width, height, totalFrames, droppedFrames, totalBytes;
        public bool active, reconnecting;
        public double congestion;
        public Flags flags;
        public object settings;

        public struct Flags
        {
            public int rawValue;
            public bool audio, video, encoded, multiTrack, service;
        }
    }

    public struct OBSStats
    {
        public double fps, averageframetime, cpuusage, memoryusage, freediskspace;
        public int rendertotalframes, rendermissedframes, outputtotalframes, outputskippedframes;
    }

    public struct VideoInfoObject
    {
        public int baseWidth, baseHeight, outputWidth, outputHeight;
        public double fps; 
        public string videoFormat, colorSpace, colorRange, scaleType;
    }

    #endregion
    #region OwnObjects

    public enum SpecialSourceType { desktop1, desktop2, mic1, mic2, mic3 };
    public struct SpecialSources
    {
        public Dictionary<SpecialSourceType, string> specialSourceNames;
    }

    public struct GetVersionObject
    {
        public double version;
        public string obsWebsocketVersion, obsStudioVersion;
        public string[] availableRequests;
    }

    public struct GetCurrentSceneObject
    {
        public string name;
        public SceneItem[] sources;
    }

    public struct GetSceneListObject
    {
        public string currentScene;
        public Scene[] scenes;
    }

    public struct GetSceneTransitionOverrideObject
    {
        public string transitionName;
        public int transitionDuration;
    }

    public struct Source
    {
        public string name, typeId, type;
    }

    public struct GetVolumeObject
    {
        public string name;
        public double volume;
        public bool muted;
    }

    public struct GetPreviewSceneObject
    {
        public string name;
        public SceneItem[] sources;
    }

    public struct GetTransitionListObject
    {
        public string currentTransition;
        public string[] transitions;
    }

    public struct GetStreamingStatusObject
    {
        public bool streaming, recording, recordingPaused, previewOnly;
        public string streamTimecode, recTimecode;
    }

    public struct Mixer
    {
        public int id;
        public bool enabled;
    }

    public struct Filter
    {
        public string name, type;
        public bool enabled;
    }


    #endregion
}
