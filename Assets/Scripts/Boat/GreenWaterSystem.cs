using UnityEngine;

namespace StormFishingVessel.Boat
{
    public class GreenWaterSystem : MonoBehaviour
    {
        public DeckFloodZone[] FloodZones;
        public float FloodChance;
        public float ActiveDuration = 6f;
        public float CooldownDuration = 8f;

        private float _timer;
        private bool _active;
        private DeckFloodZone _currentZone;

        public delegate void GreenWaterStateChanged(DeckFloodZone zone, bool active);
        public event GreenWaterStateChanged OnGreenWaterStateChanged;

        private void Update()
        {
            if (FloodZones == null || FloodZones.Length == 0)
            {
                return;
            }

            _timer += Time.deltaTime;
            if (_active && _timer >= ActiveDuration)
            {
                _active = false;
                _timer = 0f;
                if (_currentZone != null)
                {
                    _currentZone.ApplyFloodState(false);
                    OnGreenWaterStateChanged?.Invoke(_currentZone, false);
                    _currentZone = null;
                }
            }
            else if (!_active && _timer >= CooldownDuration)
            {
                _timer = 0f;
                TryActivateZone();
            }
        }

        public void SetFloodChance(float chance)
        {
            FloodChance = Mathf.Clamp01(chance);
        }

        private void TryActivateZone()
        {
            if (Random.value > FloodChance)
            {
                return;
            }

            var zone = FloodZones[Random.Range(0, FloodZones.Length)];
            _active = true;
            _currentZone = zone;
            _currentZone.ApplyFloodState(true);
            OnGreenWaterStateChanged?.Invoke(zone, true);
        }
    }
}
