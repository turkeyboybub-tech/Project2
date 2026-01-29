using UnityEngine;

namespace StormFishingVessel.UI
{
    public class SonarPingDisplay : MonoBehaviour
    {
        public AudioSource PingSource;
        public float PingInterval = 3f;
        private float _timer;

        private void Update()
        {
            if (PingSource == null)
            {
                return;
            }

            _timer += Time.deltaTime;
            if (_timer >= PingInterval)
            {
                _timer = 0f;
                PingSource.Play();
            }
        }
    }
}
