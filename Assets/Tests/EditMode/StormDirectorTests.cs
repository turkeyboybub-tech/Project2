using NUnit.Framework;
using StormFishingVessel.Core;
using UnityEngine;

namespace StormFishingVessel.Tests
{
    public class StormDirectorTests
    {
        [Test]
        public void StormDirector_ClampsIntensity()
        {
            var profile = ScriptableObject.CreateInstance<StormProfile>();
            profile.Stages = new[]
            {
                new StormStage { Name = "Stage", Intensity = 1.5f, DurationSeconds = 10f }
            };

            var go = new GameObject("StormDirector");
            var director = go.AddComponent<StormDirector>();
            director.Profile = profile;
            director.Begin(0);

            Assert.AreEqual(1f, director.NormalizedIntensity);
            Object.DestroyImmediate(go);
        }

        [Test]
        public void StormDirector_FiresStageChanged()
        {
            var profile = ScriptableObject.CreateInstance<StormProfile>();
            profile.Stages = new[]
            {
                new StormStage { Name = "Stage", Intensity = 0.2f, DurationSeconds = 10f }
            };

            var go = new GameObject("StormDirector");
            var director = go.AddComponent<StormDirector>();
            director.Profile = profile;
            var called = false;
            director.OnStageChanged += _ => called = true;

            director.Begin(0);
            Assert.IsTrue(called);
            Object.DestroyImmediate(go);
        }
    }
}
