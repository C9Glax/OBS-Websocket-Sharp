using System.Collections.Generic;

namespace OBSWebsocketSharp
{
    public struct Output
    {
        public string name, type;
        public int width, height, totalFrames, droppedFrames, totalBytes;
        public bool active, reconnecting;
        public double congestion;
    }

    public struct OBSStats
    {
        public double fps, averageframetime, cpuusage, memoryusage, freediskspace;
        public int rendertotalframes, outputtotalframes, outputskippedframes;
    }

    public struct VideoInfo
    {
        public int baseWidth, baseHeight, outputWidth, outputHeight;
        public double fps; 
        public string videoFormat, colorSpace, colorRange, scaleType;
    }

    public enum SpecialSourceType { desktop1, desktop2, mic1, mic2, mic3 };
    public struct SpecialSources
    {
        public Dictionary<SpecialSourceType, string> sources;
    }

    public struct StreamStatus
    {
        public bool streaming, recording, recordingPaused, replayBufferActive;
        public int bytesPerSec, kbitsPerSec, totalStreamTime, numTotalFrames, numDroppedFrames, renderTotalFrames, renderMissedFrames, outputTotalFrames, outputSkippedFrames;
        public double strain, fps, averageFrameTime, cpuUsage, memoryUsage, freeDiskSpace;
    }
}
