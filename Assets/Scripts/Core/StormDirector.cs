using UnityEngine;

namespace StormFishingVessel.Core
{
    public class StormDirector : MonoBehaviour
    {
        [Header("Profile")]
        public StormProfile Profile;
        public bool AutoStart = true;
        public int StartingStageIndex;

        [Header("Runtime")]
        public int CurrentStageIndex;
        public float StageTimer;
        public float NormalizedIntensity;

        public delegate void StormStageChanged(StormStage stage);
        public event StormStageChanged OnStageChanged;

        private void Start()
        {
            if (AutoStart)
            {
                Begin(StartingStageIndex);
            }
        }

        public void Begin(int stageIndex)
        {
            if (Profile == null || Profile.Stages == null || Profile.Stages.Length == 0)
            {
                return;
            }

            CurrentStageIndex = Mathf.Clamp(stageIndex, 0, Profile.Stages.Length - 1);
            StageTimer = 0f;
            NormalizedIntensity = Mathf.Clamp01(Profile.Stages[CurrentStageIndex].Intensity);
            OnStageChanged?.Invoke(Profile.Stages[CurrentStageIndex]);
        }

        private void Update()
        {
            if (Profile == null || Profile.Stages == null || Profile.Stages.Length == 0)
            {
                return;
            }

            var stage = Profile.Stages[CurrentStageIndex];
            StageTimer += Time.deltaTime;
            NormalizedIntensity = Mathf.Clamp01(stage.Intensity);

            if (StageTimer >= stage.DurationSeconds)
            {
                AdvanceStage();
            }
        }

        public void AdvanceStage()
        {
            if (Profile == null || Profile.Stages == null || Profile.Stages.Length == 0)
            {
                return;
            }

            CurrentStageIndex = Mathf.Min(CurrentStageIndex + 1, Profile.Stages.Length - 1);
            StageTimer = 0f;
            NormalizedIntensity = Mathf.Clamp01(Profile.Stages[CurrentStageIndex].Intensity);
            OnStageChanged?.Invoke(Profile.Stages[CurrentStageIndex]);
        }
    }
}
