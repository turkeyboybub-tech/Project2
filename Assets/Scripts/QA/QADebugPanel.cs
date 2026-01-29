using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StormFishingVessel.Boat;
using StormFishingVessel.Core;
using StormFishingVessel.Tasks;
using StormFishingVessel.Weather;

namespace StormFishingVessel.QA
{
    public class QADebugPanel : MonoBehaviour
    {
        [Header("Toggles")]
        public bool EnableDebugPanel = true;
        public bool EnableInDevelopmentBuild = true;

        [Header("Systems")]
        public StormDirector StormDirector;
        public GreenWaterSystem GreenWaterSystem;
        public BoatMotionSystem BoatMotionSystem;
        public WeatherSystem WeatherSystem;
        public TaskSystem TaskSystem;

        [Header("Test Props")]
        public GameObject[] TestProps;
        public Transform PropSpawnPoint;

        [Header("Teleport Targets")]
        public Transform DeckTarget;
        public Transform BridgeTarget;
        public Transform InteriorTarget;
        public Transform EngineRoomTarget;
        public Transform PlayerRig;

        private float _manualIntensity = 3f;

        private void OnGUI()
        {
            if (!EnableDebugPanel)
            {
                return;
            }

            if (!EnableInDevelopmentBuild && !Debug.isDebugBuild)
            {
                return;
            }

            GUILayout.BeginArea(new Rect(10, 10, 320, 720), "QA Debug Panel", GUI.skin.window);
            GUILayout.Label($"Scene: {SceneManager.GetActiveScene().name}");

            GUILayout.Space(6);
            GUILayout.Label("Force Load Levels");
            if (GUILayout.Button("Load Level 1"))
            {
                SceneManager.LoadScene("Level1_Crabber");
            }
            if (GUILayout.Button("Load Level 2"))
            {
                SceneManager.LoadScene("Level2_Longliner");
            }
            if (GUILayout.Button("Load Level 3"))
            {
                SceneManager.LoadScene("Level3_FactoryTrawler");
            }

            GUILayout.Space(6);
            GUILayout.Label("Storm Intensity (1-5)");
            _manualIntensity = GUILayout.HorizontalSlider(_manualIntensity, 1f, 5f);
            if (GUILayout.Button("Apply Intensity"))
            {
                ApplyManualIntensity();
            }

            GUILayout.Space(6);
            if (GUILayout.Button("Trigger Impact Slam") && BoatMotionSystem != null)
            {
                BoatMotionSystem.EnableSlams = true;
            }

            if (GUILayout.Button("Trigger Green Water") && GreenWaterSystem != null)
            {
                GreenWaterSystem.SetFloodChance(1f);
            }

            GUILayout.Space(6);
            GUILayout.Label("Weather Toggles");
            if (GUILayout.Button("Toggle Fog") && WeatherSystem != null)
            {
                WeatherSystem.FogDensity = Mathf.Approximately(WeatherSystem.FogDensity, 0f) ? 0.7f : 0f;
            }
            if (GUILayout.Button("Toggle Rain") && WeatherSystem != null)
            {
                WeatherSystem.RainRate = Mathf.Approximately(WeatherSystem.RainRate, 0f) ? 0.9f : 0f;
            }
            if (GUILayout.Button("Toggle Sleet") && WeatherSystem != null)
            {
                WeatherSystem.SleetRate = Mathf.Approximately(WeatherSystem.SleetRate, 0f) ? 0.8f : 0f;
            }

            GUILayout.Space(6);
            GUILayout.Label("Task Controls");
            if (GUILayout.Button("Advance Task") && TaskSystem != null && TaskSystem.ActiveTasks.Count > 0)
            {
                var current = TaskSystem.ActiveTasks[0];
                TaskSystem.StartTask(current.Id);
                TaskSystem.CompleteTask(current.Id);
            }

            GUILayout.Space(6);
            GUILayout.Label("Spawn Props");
            if (GUILayout.Button("Spawn Test Props"))
            {
                SpawnProps();
            }

            GUILayout.Space(6);
            GUILayout.Label("Teleport");
            if (GUILayout.Button("Deck") && DeckTarget != null)
            {
                TeleportTo(DeckTarget);
            }
            if (GUILayout.Button("Bridge") && BridgeTarget != null)
            {
                TeleportTo(BridgeTarget);
            }
            if (GUILayout.Button("Interior") && InteriorTarget != null)
            {
                TeleportTo(InteriorTarget);
            }
            if (GUILayout.Button("Engine Room") && EngineRoomTarget != null)
            {
                TeleportTo(EngineRoomTarget);
            }

            GUILayout.EndArea();
        }

        private void ApplyManualIntensity()
        {
            if (StormDirector == null || StormDirector.Profile == null)
            {
                return;
            }

            StormDirector.NormalizedIntensity = Mathf.Clamp01(_manualIntensity / 5f);
        }

        private void SpawnProps()
        {
            if (TestProps == null || TestProps.Length == 0 || PropSpawnPoint == null)
            {
                return;
            }

            foreach (var prefab in TestProps)
            {
                if (prefab == null)
                {
                    continue;
                }

                Instantiate(prefab, PropSpawnPoint.position, Quaternion.identity);
            }
        }

        private void TeleportTo(Transform target)
        {
            if (PlayerRig == null)
            {
                return;
            }

            PlayerRig.position = target.position;
            PlayerRig.rotation = target.rotation;
        }
    }
}
