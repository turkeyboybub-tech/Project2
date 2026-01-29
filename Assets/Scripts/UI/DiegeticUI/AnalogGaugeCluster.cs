using UnityEngine;

namespace StormFishingVessel.UI
{
    public class AnalogGaugeCluster : MonoBehaviour
    {
        public Transform SpeedNeedle;
        public Transform EngineNeedle;
        public float SpeedMax = 30f;
        public float EngineMax = 100f;

        public void UpdateSpeed(float knots)
        {
            if (SpeedNeedle == null)
            {
                return;
            }

            var t = Mathf.Clamp01(knots / SpeedMax);
            SpeedNeedle.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(140f, -140f, t));
        }

        public void UpdateEngine(float percent)
        {
            if (EngineNeedle == null)
            {
                return;
            }

            var t = Mathf.Clamp01(percent / EngineMax);
            EngineNeedle.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(140f, -140f, t));
        }
    }
}
