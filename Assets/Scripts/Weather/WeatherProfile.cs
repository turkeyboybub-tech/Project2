using UnityEngine;

namespace StormFishingVessel.Weather
{
    [CreateAssetMenu(menuName = "StormFishingVessel/Weather Profile")]
    public class WeatherProfile : ScriptableObject
    {
        public string ProfileId;
        public float HeavyRainRate;
        public float SleetRate;
        public float FogDensity;
        public float SpindriftRate;
        public float WetLensChance;
        public float SpotlightBloom;
        public Color DeckFloodAmber;
        public Color BridgeRed;
    }
}
