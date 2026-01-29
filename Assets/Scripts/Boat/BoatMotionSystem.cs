using UnityEngine;

namespace StormFishingVessel.Boat
{
    public class BoatMotionSystem : MonoBehaviour
    {
        public BoatMotionPreset Preset;
        public Transform BoatRoot;
        public float ComfortScale = 1f;
        public bool EnableSlams = true;

        private float _slamTimer;
        private float _rollPhase;
        private float _pitchPhase;
        private float _heavePhase;

        private void Reset()
        {
            BoatRoot = transform;
        }

        private void Update()
        {
            if (Preset == null || BoatRoot == null)
            {
                return;
            }

            _rollPhase += Time.deltaTime * Preset.RollSpeed;
            _pitchPhase += Time.deltaTime * Preset.PitchSpeed;
            _heavePhase += Time.deltaTime * Preset.HeaveSpeed;

            var roll = Mathf.Sin(_rollPhase) * Preset.RollDegrees * ComfortScale;
            var pitch = Mathf.Sin(_pitchPhase) * Preset.PitchDegrees * ComfortScale;
            var heave = Mathf.Sin(_heavePhase) * Preset.HeaveMeters * ComfortScale;

            var baseRotation = Quaternion.Euler(pitch, 0f, -roll);
            BoatRoot.localRotation = baseRotation;
            BoatRoot.localPosition = new Vector3(0f, heave, 0f);

            if (EnableSlams)
            {
                _slamTimer += Time.deltaTime;
                if (_slamTimer >= Preset.SlamCooldownSeconds)
                {
                    _slamTimer = 0f;
                    var slam = Quaternion.Euler(Preset.SlamImpulseDegrees * ComfortScale, 0f, 0f);
                    BoatRoot.localRotation = baseRotation * slam;
                }
            }
        }
    }
}
