using System.Collections;
using NUnit.Framework;
using StormFishingVessel.Boat;
using StormFishingVessel.Comfort;
using UnityEngine;
using UnityEngine.TestTools;

namespace StormFishingVessel.Tests
{
    public class ComfortControllerTests
    {
        [UnityTest]
        public IEnumerator ComfortController_UpdatesMotionScale()
        {
            var boatGo = new GameObject("Boat");
            var motion = boatGo.AddComponent<BoatMotionSystem>();
            motion.ComfortScale = 1f;

            var settings = ScriptableObject.CreateInstance<ComfortSettings>();
            settings.MotionScale = 0.5f;
            settings.EnableWetLens = false;

            var comfortGo = new GameObject("Comfort");
            var comfort = comfortGo.AddComponent<ComfortController>();
            comfort.MotionSystem = motion;
            comfort.WetLensOverlay = new GameObject("WetLens");

            comfort.ApplySettings(settings);
            yield return null;

            Assert.AreEqual(0.5f, motion.ComfortScale);
            Assert.IsFalse(comfort.WetLensOverlay.activeSelf);

            Object.Destroy(boatGo);
            Object.Destroy(comfortGo);
        }
    }
}
