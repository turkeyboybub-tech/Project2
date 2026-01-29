using System.Collections;
using NUnit.Framework;
using StormFishingVessel.Boat;
using UnityEngine;
using UnityEngine.TestTools;

namespace StormFishingVessel.Tests
{
    public class GreenWaterSystemTests
    {
        [UnityTest]
        public IEnumerator GreenWater_TriggersAndResolves()
        {
            var zoneGo = new GameObject("Zone");
            var zone = zoneGo.AddComponent<DeckFloodZone>();

            var systemGo = new GameObject("GreenWater");
            var system = systemGo.AddComponent<GreenWaterSystem>();
            system.FloodZones = new[] { zone };
            system.ActiveDuration = 0.1f;
            system.CooldownDuration = 0.1f;
            system.SetFloodChance(1f);

            var activated = false;
            var deactivated = false;
            system.OnGreenWaterStateChanged += (_, active) =>
            {
                if (active)
                {
                    activated = true;
                }
                else
                {
                    deactivated = true;
                }
            };

            yield return new WaitForSeconds(0.25f);

            Assert.IsTrue(activated);
            Assert.IsTrue(deactivated);

            Object.Destroy(zoneGo);
            Object.Destroy(systemGo);
        }
    }
}
