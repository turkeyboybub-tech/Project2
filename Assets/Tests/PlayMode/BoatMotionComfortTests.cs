using System.Collections;
using NUnit.Framework;
using StormFishingVessel.Boat;
using UnityEngine;
using UnityEngine.TestTools;

namespace StormFishingVessel.Tests
{
    public class BoatMotionComfortTests
    {
        [UnityTest]
        public IEnumerator BoatMotionSystem_DoesNotAttachToCamera()
        {
            var cameraGO = new GameObject("VRCamera");
            cameraGO.AddComponent<Camera>();

            var boatGO = new GameObject("BoatRoot");
            var preset = ScriptableObject.CreateInstance<BoatMotionPreset>();
            preset.RollDegrees = 10f;
            preset.PitchDegrees = 5f;
            preset.HeaveMeters = 1f;
            preset.RollSpeed = 1f;
            preset.PitchSpeed = 1f;
            preset.HeaveSpeed = 1f;

            var motion = boatGO.AddComponent<BoatMotionSystem>();
            motion.Preset = preset;
            motion.BoatRoot = boatGO.transform;

            yield return null;

            Assert.IsNull(motion.BoatRoot.GetComponent<Camera>());

            Object.Destroy(cameraGO);
            Object.Destroy(boatGO);
        }
    }
}
