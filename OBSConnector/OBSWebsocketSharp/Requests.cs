using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OBSWebsocketSharp
{
    public partial class OBSConnector
    {

        #region from https://github.com/Palakis/obs-websocket/blob/4.x-current/docs/generated/protocol.md
        public GetVersionObject GetVersion()
        {

            JObject response = this.Request("GetVersion");
            return new GetVersionObject() //no supported-image-export-formats
            {
                obsStudioVersion = response["obs-studio-version"].ToObject<string>(),
                version = response["version"].ToObject<double>(),
                availableRequests = response["available-requests"].ToObject<string>().Split(',')
            };
        }

        public void SetHeartbeat(bool enable)
        {
            _ = this.Request("SetHeartbeat", "enable", enable);
        }

        public void SetFilenameFormatting(string filenameFormatting)
        {
            _ = this.Request("SetFilenameFormatting", "filename-formatting", filenameFormatting);
        }

        public string GetFilenameFormatting()
        {
            JObject response = this.Request("GetFilenameFormatting");
            return response["filename-formatting"].ToObject<string>();
        }

        public OBSStats GetStats()
        {
            JObject response = this.Request("GetStats");
            return new OBSStats()
            {
                averageframetime = response["stats"]["average-frame-time"].ToObject<double>(),
                cpuusage = response["stats"]["cpu-usage"].ToObject<double>(),
                fps = response["stats"]["fps"].ToObject<double>(),
                freediskspace = response["stats"]["free-disk-space"].ToObject<double>(),
                memoryusage = response["stats"]["memory-usage"].ToObject<double>(),
                outputskippedframes = response["stats"]["output-skipped-frames"].ToObject<int>(),
                outputtotalframes = response["stats"]["output-total-frames"].ToObject<int>(),
                rendertotalframes = response["stats"]["render-total-frames"].ToObject<int>()
            };
        }

        public VideoInfoObject GetVideoInfo()
        {
            JObject response = this.Request("GetVideoInfo");
            return new VideoInfoObject()
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

        public void OpenProjector() //No Request Fields
        {
            _ = this.Request("OpenProjector");
        }

        public Output[] ListOutputs()
        {
            JObject response = this.Request("ListOutputs");
            List<Output> ret = new List<Output>();
            foreach(JObject output in response["outputs"])
                ret.Add(new Output()
                {
                    active = output["active"].ToObject<bool>(),
                    congestion = output["congestion"].ToObject<double>(),
                    droppedFrames = output["droppedFrames"].ToObject<int>(),
                    height = output["height"].ToObject<int>(),
                    width = output["width"].ToObject<int>(),
                    name = output["name"].ToObject<string>(),
                    reconnecting = output["reconnecting"].ToObject<bool>(),
                    totalBytes = output["totalBytes"].ToObject<int>(),
                    totalFrames = output["totalFrames"].ToObject<int>(),
                    type = output["type"].ToObject<string>()
                });
            return ret.ToArray();
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

        public void SetCurrentProfile(string profileName)
        {
            _ = this.Request("SetCurrentProfile", "profile-name", profileName);
        }

        public string GetCurrentProfile()
        {
            JObject response = this.Request("GetCurrentProfile");
            return response["profile-name"].ToObject<string>();
        }

        public string[] ListProfiles()
        {
            JObject response = this.Request("GetCurrentProfile");
            List<string> profiles = new List<string>();
            foreach (JObject profile in response["profiles"])
                profiles.Add(profile["profile-name"].ToObject<string>());
            return profiles.ToArray();
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
            List<string> sceneCollections = new List<string>();
            foreach (JObject sceneCollection in response["scene-collections"])
                sceneCollections.Add(sceneCollection["sc-name"].ToObject<string>());
            return sceneCollections.ToArray();
        }

        public void SetCurrentScene(string scenename)
        {
            _ = this.Request("SetCurrentScene", "scene-name", scenename);
        }

        public GetCurrentSceneObject GetCurrentScene()
        {
            JObject response = this.Request("GetCurrentScene");
            List<SceneItem> sources = new List<SceneItem>();
            foreach (JObject source in response["sources"])
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
                    source_cx = source["sceneitem_cx"].ToObject<int>(),
                    source_cy = source["sceneitem_cy"].ToObject<int>()
                });
            return new GetCurrentSceneObject()
            {
                name = response["name"].ToObject<string>(),
                sources = sources.ToArray()
            };
        }

        public GetSceneListObject GetSceneList()
        {
            JObject response = this.Request("GetSceneList");
            List<Scene> scenes = new List<Scene>();
            foreach (JObject scene in response["scenes"])
            {
                List<SceneItem> sources = new List<SceneItem>();
                foreach (JObject source in scene["sources"])
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
                        source_cx = source["sceneitem_cx"].ToObject<int>(),
                        source_cy = source["sceneitem_cy"].ToObject<int>()
                    });
                scenes.Add(new Scene()
                {
                    name = scene["name"].ToObject<string>(),
                    sources = sources.ToArray()
                });
            }
                
            return new GetSceneListObject()
            {
                currentScene = response["current-scene"].ToObject<string>(),
                scenes = scenes.ToArray()
            };
        }

        public void SetSceneTransitionOverride(string sceneName, string transitionName)
        {
            _ = this.Request("SetSceneTransitionOverride", "sceneName", sceneName, "transitionName", transitionName);
        }

        public void SetSceneTransitionOverride(string sceneName, string transitionName, int transitionDuration)
        {
            _ = this.Request("SetSceneTransitionOverride", "sceneName", sceneName, "transitionName", transitionName, "transitionDuration", transitionDuration);
        }

        public void RemoveSceneTransitionOverride(string sceneName)
        {
            _ = this.Request("SetSceneTransitionOverride", "sceneName", sceneName);
        }

        public GetSceneTransitionOverrideObject GetSceneTransitionOverride(string sceneName)
        {
            JObject response = this.Request("GetSceneTransitionOverride", "sceneName", sceneName);
            return new GetSceneTransitionOverrideObject()
            {
                transitionDuration = response["transitionDuration"].ToObject<int>(),
                transitionName = response["transitionName"].ToObject<string>()
            };
        }

        public Source[] GetSourcesList()
        {
            JObject response = this.Request("GetSourcesList");
            List<Source> sources = new List<Source>();
            foreach (JObject source in response["sources"])
                source.Add(new Source()
                {
                    name = source["name"].ToObject<string>(),
                    typeId = source["typeId"].ToObject<string>(),
                    type = source["type"].ToObject<string>()
                });
            return sources.ToArray();
        }

        public GetVolumeObject GetVolume(string source)
        {
            JObject response = this.Request("GetVolume", "source", source);
            return new GetVolumeObject()
            {
                volume = response["volume"].ToObject<double>(),
                muted = response["muted"].ToObject<bool>(),
                name = response["name"].ToObject<string>()
            }; 
        }

        public GetVolumeObject GetVolume(string source, bool useDecible)
        {
            JObject response = this.Request("GetVolume", "source", source, "useDecible", useDecible);
            return new GetVolumeObject()
            {
                volume = response["volume"].ToObject<double>(),
                muted = response["muted"].ToObject<bool>(),
                name = response["name"].ToObject<string>()
            };
        }

        public void SetVolume(string source, double volume)
        {
            _ = this.Request("SetVolume", "source", source, "volume", volume);
        }

        public void SetVolume(string source, double volume, bool useDecible)
        {
            _ = this.Request("SetVolume", "source", source, "volume", volume, "useDecible", useDecible);
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

        public void SetSourceName(string sourceName, string newName)
        {
            _ = this.Request("SetSourceName", "sourceName", sourceName, "newName", newName);
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
                specialSourceNames = new Dictionary<SpecialSourceType, string>(){
                    { SpecialSourceType.desktop1, response.ContainsKey("desktop-1") ? response["desktop-1"].ToObject<string>() : string.Empty },
                    { SpecialSourceType.desktop2, response.ContainsKey("desktop-2") ? response["desktop-2"].ToObject<string>() : string.Empty },
                    { SpecialSourceType.mic1, response.ContainsKey("mic-1") ? response["mic-1"].ToObject<string>() : string.Empty },
                    { SpecialSourceType.mic2, response.ContainsKey("mic-2") ? response["mic-2"].ToObject<string>() : string.Empty },
                    { SpecialSourceType.mic3, response.ContainsKey("mic-3") ? response["mic-3"].ToObject<string>() : string.Empty }
                }
                
            };
        }

        public Filter[] GetSourceFilters(string sourceName) //No settings
        {
            JObject response = this.Request("GetSourceFilters", "sourceName", sourceName);
            List<Filter> filters = new List<Filter>();
            foreach (JObject filter in response["filters"])
                filters.Add(new Filter()
                {
                    enabled = filter["enabled"].ToObject<bool>(),
                    type = filter["type"].ToObject<string>(),
                    name = filter["name"].ToObject<string>()
                });
            return filters.ToArray();
        }

        public Filter GetSourceFilterInfo(string sourceName, string filterName)
        {
            JObject response = this.Request("GetSourceFilterInfo", "sourceName", sourceName, "filterName", filterName);
            return new Filter()
            {
                enabled = response["enabled"].ToObject<bool>(),
                type = response["type"].ToObject<string>(),
                name = response["name"].ToObject<string>()
            };
        }

        public void RemoveFilterFromSource(string sourceName, string filterName)
        {
            _ = this.Request("RemoveFilterFromSource", "sourceName", sourceName, "filterName", filterName);
        }

        public void ReorderSourceFilter(string sourceName, string filterName, int newIndex)
        {
            _ = this.Request("ReorderSourceFilter", "sourceName", sourceName, "filterName", filterName, "newIndex", newIndex);
        }

        public void SetSourceFilterVisibility(string sourceName, string filterName, bool filterEnabled)
        {
            _ = this.Request("SetSourceFilterVisibility", "sourceName", sourceName, "filterName", filterName, "filterEnabled", filterEnabled);
        }

        public string GetAudioMonitorType(string sourceName)
        {
            JObject response = this.Request("GetAudioMonitorType", "sourceName", sourceName);
            return response["monitorType"].ToObject<string>();
        }

        public void SetAudioMonitorType(string sourceName, string monitorType)
        {
            _ = this.Request("SetAudioMonitorType", "sourceName", sourceName, "monitorType", monitorType);
        }

        public GetStreamingStatusObject GetStreamingStatus()
        {
            JObject response = this.Request("GetStreamingStatus");
            return new GetStreamingStatusObject()
            {
                recording = response["recording"].ToObject<bool>(),
                recordingPaused = response["recording-paused"].ToObject<bool>(),
                streaming = response["streaming"].ToObject<bool>(),
                previewOnly = response["preview-only"].ToObject<bool>(),
                streamTimecode = response["stream-timecode"].ToObject<string>(),
                recTimecode = response["rec -timecode"].ToObject<string>()
            };
        }

        public void StartStopStreaming()
        {
            _ = this.Request("StartStopStreaming");
        }

        public void StartStreaming() //No Fields
        {
            _ = this.Request("StartStreaming");
        }

        public void StopStreaming()
        {
            _ = this.Request("StopStreaming");
        }

        public void SendCaptions(string text)
        {
            _ = this.Request("SendCaptions", "text", text);
        }

        public bool GetStudioModeStatus()
        {
            JObject response = this.Request("GetStudioModeStatus");
            return response["studio-mode"].ToObject<bool>();
        }

        public GetPreviewSceneObject GetPreviewScene()
        {
            JObject response = this.Request("GetPreviewScene");
            List<SceneItem> sources = new List<SceneItem>();
            foreach (JObject source in response["sources"])
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
                    source_cx = source["sceneitem_cx"].ToObject<int>(),
                    source_cy = source["sceneitem_cy"].ToObject<int>()
                });
            return new GetPreviewSceneObject()
            {
                name = response["name"].ToObject<string>(),
                sources = sources.ToArray()
            };
        }

        public void SetPreviewScene(string sceneName)
        {
            _ = this.Request("SetPreviewScene", "scene-name", sceneName);
        }

        public void TransitionToProgram() //No specific transition
        {
            _ = this.Request("TransitionToProgram");
        }

        public void EnableStudioMode()
        {
            _ = this.Request("EnableStudioMode");
        }
        
        public void DisableStudioMode()
        {
            _ = this.Request("DisableStudioMode");
        }

        public void ToggleStudioMode()
        {
            _ = this.Request("ToggleStudioMode");
        }

        public GetTransitionListObject GetTransitionList()
        {
            JObject response = this.Request("GetTransitionList");
            List<string> transitions = new List<string>();
            foreach (JObject transition in response["transitions"])
                transitions.Add(transition["name"].ToObject<string>());
            return new GetTransitionListObject()
            {
                currentTransition = response["current-transition"].ToObject<string>(),
                transitions = transitions.ToArray()
            };
        }

        public string GetCurrentTransition()
        {
            JObject response = this.Request("GetCurrentTransition");
            return response["name"].ToObject<string>();
        }

        public void SetCurrentTransition(string transitionName)
        {
            _ = this.Request("SetCurrentTransition", "transition-name", transitionName);
        }

        public void SetTransitionDuration(int duration)
        {
            _ = this.Request("SetTransitionDuration", "duration", duration);
        }

        public int GetTransitionDuration()
        {
            JObject response = this.Request("GetTransitionDuration");
            return response["transition-duration"].ToObject<int>();
        }

        public double GetTransitionPosition()
        {
            JObject response = this.Request("GetTransitionPosition");
            return response["position"].ToObject<double>();
        }

        #endregion

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
