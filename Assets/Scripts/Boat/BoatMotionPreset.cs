using UnityEngine;

namespace StormFishingVessel.Boat
{
    [CreateAssetMenu(menuName = "StormFishingVessel/Boat Motion Preset")]
    public class BoatMotionPreset : ScriptableObject
    {
        public string PresetName;
        [Range(0f, 30f)] public float RollDegrees;
        [Range(0f, 20f)] public float PitchDegrees;
        [Range(0f, 2f)] public float HeaveMeters;
        public float RollSpeed;
        public float PitchSpeed;
        public float HeaveSpeed;
        public float SlamImpulseDegrees;
        public float SlamCooldownSeconds;
    }
}
