using System.Collections;
using NUnit.Framework;
using StormFishingVessel.UI;
using UnityEngine;
using UnityEngine.TestTools;

namespace StormFishingVessel.Tests
{
    public class DiegeticUITests
    {
        [UnityTest]
        public IEnumerator RadarGlowPulses()
        {
            var go = new GameObject("Radar");
            var light = go.AddComponent<Light>();
            var radar = go.AddComponent<RadarDisplay>();
            radar.RadarGlow = light;

            var initial = light.intensity;
            yield return new WaitForSeconds(0.2f);
            Assert.AreNotEqual(initial, light.intensity);

            Object.Destroy(go);
        }

        [Test]
        public void NoHudCanvasPresent()
        {
            var canvases = Object.FindObjectsOfType<Canvas>();
            foreach (var canvas in canvases)
            {
                Assert.AreNotEqual(RenderMode.ScreenSpaceOverlay, canvas.renderMode);
            }
        }
    }
}
