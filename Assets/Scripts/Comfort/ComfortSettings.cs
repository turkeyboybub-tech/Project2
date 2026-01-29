using UnityEngine;

namespace StormFishingVessel.Comfort
{
    [CreateAssetMenu(menuName = "StormFishingVessel/Comfort Settings")]
    public class ComfortSettings : ScriptableObject
    {
        public string PresetName;
        [Range(0.4f, 1f)] public float MotionScale = 1f;
        public bool EnableVignette = true;
        public bool EnableHorizonAid = true;
        public bool EnableWetLens = true;
        public bool SnapTurn = true;
    }
}
