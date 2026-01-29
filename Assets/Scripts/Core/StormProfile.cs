using System;
using UnityEngine;

namespace StormFishingVessel.Core
{
    [Serializable]
    public class StormStage
    {
        public string Name;
        [Range(0f, 1f)] public float Intensity;
        public float DurationSeconds;
        public float WindSpeedKnots;
        public float WaveHeightMeters;
        public float FogDensity;
        public float RainRate;
        public float SleetRate;
        public float SpindriftRate;
        public float DeckFloodChance;
        public string MoodTag;
    }

    [CreateAssetMenu(menuName = "StormFishingVessel/Storm Profile")]
    public class StormProfile : ScriptableObject
    {
        public string ProfileId;
        public string VesselClass;
        public string LevelName;
        public Color DeckFloodAmber;
        public Color BridgeRed;
        public float SpotlightConeAngle;
        public StormStage[] Stages;
    }
}
