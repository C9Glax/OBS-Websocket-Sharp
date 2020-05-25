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

    public struct SpecialSources
    {
        public string desktop1, desktop2, mic1, mic2, mic3;
    }

    public struct StreamingStatus
    {
        public bool streaming, recording, previewonly;
        public string streamtimecode, rectimecode;
    }
}
