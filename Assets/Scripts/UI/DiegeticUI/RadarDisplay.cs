using UnityEngine;

namespace StormFishingVessel.UI
{
    public class RadarDisplay : MonoBehaviour
    {
        public Light RadarGlow;
        public float PulseSpeed = 1.6f;
        public float MinIntensity = 0.4f;
        public float MaxIntensity = 1.2f;

        private void Update()
        {
            if (RadarGlow == null)
            {
                return;
            }

            var pulse = (Mathf.Sin(Time.time * PulseSpeed) + 1f) * 0.5f;
            RadarGlow.intensity = Mathf.Lerp(MinIntensity, MaxIntensity, pulse);
        }
    }
}
