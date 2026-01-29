using System;
using UnityEngine;

namespace StormFishingVessel.Tasks
{
    [Serializable]
    public class TaskDefinition
    {
        public string Id;
        public string Title;
        public string Description;
        public string VesselClass;
        public string Location;
        public string MoodTag;
        public string[] StormStages;
        public string[] Prerequisites;
        public float BaseDuration;
    }
}
