using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace OBSWebsocketSharp
{
    public partial class OBSConnector
    {
        public OBSStats GetStats()
        {
            JObject response = this.Request("GetStats");
            return new OBSStats()
            {
                averageframetime = Convert.ToDouble(response["stats"]["average-frame-time"].ToString()),
                cpuusage = Convert.ToDouble(response["stats"]["cpu-usage"].ToString()),
                fps = Convert.ToDouble(response["stats"]["fps"].ToString()),
                freediskspace = Convert.ToDouble(response["stats"]["free-disk-space"].ToString()),
                memoryusage = Convert.ToDouble(response["stats"]["memory-usage"].ToString()),
                outputskippedframes = Convert.ToInt32(response["stats"]["output-skipped-frames"].ToString()),
                outputtotalframes = Convert.ToInt32(response["stats"]["output-total-frames"].ToString()),
                rendertotalframes = Convert.ToInt32(response["stats"]["render-total-frames"].ToString())
            };
        }

        public VideoInfo GetVideoInfo()
        {
            JObject response = this.Request("GetVideoInfo");
            return new VideoInfo()
            {
                baseHeight = response["baseHeight"].ToObject<int>(),
                baseWidth = response["baseWidth"].ToObject<int>(),
                colorRange = response["colorRange"].ToObject<string>(),
                colorSpace = response["colorSpace"].ToObject<string>(),
                fps = response["fps"].ToObject<double>(),
                outputHeight = response["outputHeight"].ToObject<int>(),
                outputWidth = response["outputWidth"].ToObject<int>(),
                scaleType = response["scaleType"].ToObject<string>(),
                videoFormat = response["videoFormat"].ToObject<string>()
            };
        }

        public Output[] ListOutputs()
        {
            JObject response = this.Request("ListOutputs");
            JToken[] outputs = response["outputs"].ToArray();
            Output[] ret = new Output[outputs.Length];
            for (short i = 0; i < outputs.Length; i++)
            {
                ret[i] = new Output()
                {
                    active = outputs[i]["active"].ToObject<bool>(),
                    congestion = outputs[i]["congestion"].ToObject<double>(),
                    droppedFrames = outputs[i]["droppedFrames"].ToObject<int>(),
                    height = outputs[i]["height"].ToObject<int>(),
                    width = outputs[i]["width"].ToObject<int>(),
                    name = outputs[i]["name"].ToObject<string>(),
                    reconnecting = outputs[i]["reconnecting"].ToObject<bool>(),
                    totalBytes = outputs[i]["totalBytes"].ToObject<int>(),
                    totalFrames = outputs[i]["totalFrames"].ToObject<int>(),
                    type = outputs[i]["type"].ToObject<string>()
                };
            }
            return ret;
        }

        public Output GetOutputInfo(string outputName)
        {
            JObject response = this.Request("GetOutputInfo", "outputName", outputName);
            return new Output()
            {
                active = response["outputInfo"]["active"].ToObject<bool>(),
                congestion = response["outputInfo"]["congestion"].ToObject<double>(),
                droppedFrames = response["outputInfo"]["droppedFrames"].ToObject<int>(),
                height = response["outputInfo"]["height"].ToObject<int>(),
                width = response["outputInfo"]["width"].ToObject<int>(),
                name = response["outputInfo"]["name"].ToObject<string>(),
                reconnecting = response["outputInfo"]["reconnecting"].ToObject<bool>(),
                totalBytes = response["outputInfo"]["totalBytes"].ToObject<int>(),
                totalFrames = response["outputInfo"]["totalFrames"].ToObject<int>(),
                type = response["outputInfo"]["type"].ToObject<string>()
            };
        }

        public void StartOutput()
        {
            _ = this.Request("StartOutput");
        }

        public void StopOutput()
        {
            _ = this.Request("StopOutput");
        }
        public void StopOutput(bool force)
        {
            _ = this.Request("StopOutput", "force", force);
        }

        public void StartStopRecording()
        {
            _ = this.Request("StartStopRecording");
        }

        public void StartRecording()
        {
            _ = this.Request("StartRecording");
        }

        public void StopRecording()
        {
            _ = this.Request("StopRecording");
        }

        public void PauseRecording()
        {
            _ = this.Request("PauseRecording");
        }

        public void ResumeRecording()
        {
            _ = this.Request("ResumeRecording");
        }

        public void SetRecordingFolder(string recfolder)
        {
            _ = this.Request("SetRecordingFolder", "rec-folder", recfolder);
        }

        public string GetRecordingFolder()
        {
            JObject response = this.Request("GetRecordingFolder");
            return response["rec-folder"].ToObject<string>();

        }

        public void StartStopReplayBuffer()
        {
            _ = this.Request("StartStopReplayBuffer");
        }

        public void StartReplayBuffer()
        {
            _ = this.Request("StartReplayBuffer");
        }
        public void StopReplayBuffer()
        {
            _ = this.Request("StopReplayBuffer");
        }

        public void SaveReplayBuffer()
        {
            _ = this.Request("SaveReplayBuffer");
        }

        public void SetCurrentSceneCollection(string scname)
        {
            _ = this.Request("SetCurrentSceneCollection", "sc-name", scname);
        }
        public string GetCurrentSceneCollection()
        {
            JObject response = this.Request("GetCurrentSceneCollection");
            return response["sc-name"].ToObject<string>();
        }

        public string[] ListSceneCollections()
        {
            JObject response = this.Request("ListSceneCollections");
            JToken[] outputs = response["scene-collections"].ToArray();
            string[] ret = new string[outputs.Length];
            for (short i = 0; i < outputs.Length; i++)
                ret[i] = outputs[i].ToObject<string>();
            return ret;
        }

        public void SetCurrentScene(string scenename)
        {
            _ = this.Request("SetCurrentScene", "scene-name", scenename);
        }

        public string GetCurrentScene()
        {
            JObject response = this.Request("GetCurrentScene");
            return response["name"].ToObject<string>();
        }

        public string[] GetSceneList()
        {
            JObject response = this.Request("GetSceneList");
            JToken[] outputs = response["scenes"].ToArray();
            string[] ret = new string[outputs.Length];
            for (short i = 0; i < outputs.Length; i++)
                ret[i] = outputs[i]["name"].ToObject<string>();
            return ret;
        }

        public double GetVolume(string source)
        {
            JObject response = this.Request("GetVolume", "source", source);
            return response["volume"].ToObject<double>();
        }

        public void SetVolume(string source, double volume)
        {
            _ = this.Request("SetVolume", "source", source, "volume", volume);
        }

        public bool GetMute(string source)
        {
            JObject response = this.Request("GetMute", "source", source);
            return response["muted"].ToObject<bool>();
        }

        public void SetMute(string source, bool mute)
        {
            _ = this.Request("SetMute", "source", source, "mute", mute); ;
        }

        public void ToggleMute(string source)
        {
            _ = this.Request("ToggleMute", "source", source);
        }

        public void SetSyncOffset(string source, int offset)
        {
            _ = this.Request("SetSyncOffset", "source", source, "offset", offset);
        }

        public int GetSyncOffset(string source)
        {
            JObject response = this.Request("GetSyncOffset", "source", source);
            return response["offset"].ToObject<int>();
        }

        public SpecialSources GetSpecialSources()
        {
            JObject response = this.Request("GetSpecialSources");
            return new SpecialSources()
            {
                sources = new System.Collections.Generic.Dictionary<SpecialSourceType, string>(){
                    { SpecialSourceType.desktop1, response["desktop-1"].ToObject<string>() },
                    { SpecialSourceType.desktop2, response["desktop-2"].ToObject<string>() },
                    { SpecialSourceType.mic1, response["mic-1"].ToObject<string>() },
                    { SpecialSourceType.mic2, response["mic-2"].ToObject<string>() },
                    { SpecialSourceType.mic3, response["mic-3"].ToObject<string>() }
                }
                
            };
        }

        public StreamStatus GetStreamingStatus()
        {
            JObject response = this.Request("GetStreamingStatus");
            return new StreamStatus()
            {
                recording = response["recording"].ToObject<bool>(),
                recordingPaused = response["recording-paused"].ToObject<bool>(),
                streaming = response["streaming"].ToObject<bool>(),
            };
        }

        public void StartStopStreaming()
        {
            _ = this.Request("StartStopStreaming");
        }

        public void StartStreaming()
        {
            _ = this.Request("StartStreaming");
        }

        public void StopStreaming()
        {
            _ = this.Request("StopStreaming");
        }

        public JObject GetSourceSettings(string sourceName)
        {
            JObject response = this.Request("GetSourceSettings", "sourceName", sourceName);
            return response;
        }

        public string GetPIDOfAudioDevice(string sourceName)
        {
            JObject response = this.Request("GetSourceSettings", "sourceName", sourceName);
            return response["sourceSettings"]["device_id"].ToObject<string>();

        }
    }
}
