using UnityEngine;
using StormFishingVessel.Core;

namespace StormFishingVessel.Weather
{
    public class WeatherSystem : MonoBehaviour
    {
        public WeatherProfile Profile;
        public StormDirector StormDirector;

        [Header("Runtime")]
        [Range(0f, 1f)] public float RainRate;
        [Range(0f, 1f)] public float SleetRate;
        [Range(0f, 1f)] public float FogDensity;
        [Range(0f, 1f)] public float SpindriftRate;
        [Range(0f, 1f)] public float WetLensChance;

        private void OnEnable()
        {
            if (StormDirector != null)
            {
                StormDirector.OnStageChanged += ApplyStormStage;
            }
        }

        private void OnDisable()
        {
            if (StormDirector != null)
            {
                StormDirector.OnStageChanged -= ApplyStormStage;
            }
        }

        private void Start()
        {
            if (StormDirector != null && StormDirector.Profile != null)
            {
                ApplyStormStage(StormDirector.Profile.Stages[StormDirector.CurrentStageIndex]);
            }
        }

        public void ApplyStormStage(StormStage stage)
        {
            if (Profile == null || stage == null)
            {
                return;
            }

            RainRate = Mathf.Clamp01(stage.RainRate);
            SleetRate = Mathf.Clamp01(stage.SleetRate);
            FogDensity = Mathf.Clamp01(stage.FogDensity);
            SpindriftRate = Mathf.Clamp01(stage.SpindriftRate);
            WetLensChance = Mathf.Clamp01(Profile.WetLensChance * stage.Intensity);
        }
    }
}
