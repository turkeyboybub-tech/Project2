using UnityEngine;

namespace StormFishingVessel.Levels
{
    [CreateAssetMenu(menuName = "StormFishingVessel/Vessel Level Config")]
    public class VesselLevelConfig : ScriptableObject
    {
        public string VesselClass;
        public string LevelName;
        public string TimeOfDay;
        public string StormProfileId;
        public VesselCompartment[] Compartments;
        public string[] SafetyEquipment;
    }
}
