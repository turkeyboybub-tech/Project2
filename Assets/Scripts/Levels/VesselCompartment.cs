using System;
using UnityEngine;

namespace StormFishingVessel.Levels
{
    [Serializable]
    public class VesselCompartment
    {
        public string Name;
        public string Deck;
        public Vector3 LocalPosition;
        public string[] Equipment;
    }
}
