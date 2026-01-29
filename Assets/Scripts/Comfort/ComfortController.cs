using UnityEngine;
using StormFishingVessel.Boat;

namespace StormFishingVessel.Comfort
{
    public class ComfortController : MonoBehaviour
    {
        public ComfortSettings CurrentSettings;
        public BoatMotionSystem MotionSystem;
        public GameObject Vignette;
        public GameObject HorizonAid;
        public GameObject WetLensOverlay;

        public void ApplySettings(ComfortSettings settings)
        {
            if (settings == null)
            {
                return;
            }

            CurrentSettings = settings;
            if (MotionSystem != null)
            {
                MotionSystem.ComfortScale = settings.MotionScale;
            }

            if (Vignette != null)
            {
                Vignette.SetActive(settings.EnableVignette);
            }

            if (HorizonAid != null)
            {
                HorizonAid.SetActive(settings.EnableHorizonAid);
            }

            if (WetLensOverlay != null)
            {
                WetLensOverlay.SetActive(settings.EnableWetLens);
            }
        }
    }
}
