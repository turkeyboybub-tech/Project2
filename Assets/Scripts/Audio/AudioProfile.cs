using UnityEngine;

namespace StormFishingVessel.Audio
{
    [CreateAssetMenu(menuName = "StormFishingVessel/Audio Profile")]
    public class AudioProfile : ScriptableObject
    {
        public string ProfileId;
        public AudioClip ExteriorWind;
        public AudioClip InteriorMuffledWind;
        public AudioClip WaveSlams;
        public AudioClip EngineStrain;
        public AudioClip AlarmLoop;
        public AudioClip RadioChatter;
        public float ExteriorVolume = 1f;
        public float InteriorVolume = 0.65f;
        public float AlarmVolume = 0.9f;
    }
}
