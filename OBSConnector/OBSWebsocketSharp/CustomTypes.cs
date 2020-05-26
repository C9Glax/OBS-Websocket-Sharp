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

    public enum sourceType { desktop1, desktop2, mic1, mic2, mic3 };
    public struct SpecialSources
    {
        public Dictionary<sourceType, string> sources;
        public SpecialSources(string desktop1SourceName, string desktop2SourceName, string mic1SourceName, string mic2SourceName, string mic3SourceName)
        {
            sources = new Dictionary<sourceType, string>();
            sources.Add(sourceType.desktop1, desktop1SourceName);
            sources.Add(sourceType.desktop2, desktop2SourceName);
            sources.Add(sourceType.mic1, mic1SourceName);
            sources.Add(sourceType.mic2, mic2SourceName);
            sources.Add(sourceType.mic3, mic3SourceName);
        }
    }

    public struct StreamingStatus
    {
        public bool streaming, recording, previewonly;
        public string streamtimecode, rectimecode;
    }
}
