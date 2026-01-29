using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StormFishingVessel.Boat;

namespace StormFishingVessel.QA
{
    public class PerformanceTestMode : MonoBehaviour
    {
        public bool EnablePerformanceTest;
        public string[] LevelScenes = { "Level1_Crabber", "Level2_Longliner", "Level3_FactoryTrawler" };
        public float RunMinutes = 3f;
        public float GreenWaterInterval = 20f;
        public PerformanceMetricsSampler Sampler;
        public GreenWaterSystem GreenWaterSystem;

        private bool _running;

        private void Start()
        {
            if (EnablePerformanceTest)
            {
                StartCoroutine(RunPerformanceSuite());
            }
        }

        private IEnumerator RunPerformanceSuite()
        {
            if (_running)
            {
                yield break;
            }

            _running = true;
            foreach (var level in LevelScenes)
            {
                yield return SceneManager.LoadSceneAsync(level);
                yield return RunLevelPass(level, 0.6f);
                yield return RunLevelPass(level, 1f);
            }

            _running = false;
        }

        private IEnumerator RunLevelPass(string levelName, float intensity)
        {
            if (Sampler == null)
            {
                yield break;
            }

            Sampler.StartSampling();
            var elapsed = 0f;
            var greenWaterTimer = 0f;

            while (elapsed < RunMinutes * 60f)
            {
                elapsed += Time.unscaledDeltaTime;
                greenWaterTimer += Time.unscaledDeltaTime;

                if (greenWaterTimer >= GreenWaterInterval)
                {
                    greenWaterTimer = 0f;
                    if (GreenWaterSystem != null)
                    {
                        GreenWaterSystem.SetFloodChance(1f);
                    }
                }

                yield return null;
            }

            Sampler.StopSampling();
            var report = Sampler.BuildReport($"{levelName}_intensity_{intensity}");
            TestReportWriter.WritePerformanceReport(report);
        }
    }
}
