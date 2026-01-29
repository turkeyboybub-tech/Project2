using UnityEngine;
using StormFishingVessel.Core;

namespace StormFishingVessel.Audio
{
    public class AudioSystem : MonoBehaviour
    {
        public AudioProfile Profile;
        public StormDirector StormDirector;
        public AudioSource ExteriorSource;
        public AudioSource InteriorSource;
        public AudioSource AlarmSource;
        public AudioSource RadioSource;

        private void Start()
        {
            if (Profile == null)
            {
                return;
            }

            ConfigureSources();
        }

        private void ConfigureSources()
        {
            if (ExteriorSource != null)
            {
                ExteriorSource.clip = Profile.ExteriorWind;
                ExteriorSource.loop = true;
                ExteriorSource.volume = Profile.ExteriorVolume;
                ExteriorSource.Play();
            }

            if (InteriorSource != null)
            {
                InteriorSource.clip = Profile.InteriorMuffledWind;
                InteriorSource.loop = true;
                InteriorSource.volume = Profile.InteriorVolume;
                InteriorSource.Play();
            }

            if (AlarmSource != null)
            {
                AlarmSource.clip = Profile.AlarmLoop;
                AlarmSource.loop = true;
                AlarmSource.volume = Profile.AlarmVolume;
            }

            if (RadioSource != null)
            {
                RadioSource.clip = Profile.RadioChatter;
                RadioSource.loop = true;
            }
        }

        private void Update()
        {
            if (StormDirector == null || Profile == null)
            {
                return;
            }

            var intensity = StormDirector.NormalizedIntensity;
            if (ExteriorSource != null)
            {
                ExteriorSource.volume = Mathf.Lerp(0.4f, Profile.ExteriorVolume, intensity);
            }

            if (InteriorSource != null)
            {
                InteriorSource.volume = Mathf.Lerp(0.2f, Profile.InteriorVolume, intensity);
            }

            if (AlarmSource != null)
            {
                AlarmSource.volume = Mathf.Lerp(0f, Profile.AlarmVolume, intensity);
                if (!AlarmSource.isPlaying && intensity > 0.6f)
                {
                    AlarmSource.Play();
                }
            }
        }
    }
}
