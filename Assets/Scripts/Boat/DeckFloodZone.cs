using UnityEngine;

namespace StormFishingVessel.Boat
{
    public class DeckFloodZone : MonoBehaviour
    {
        public string ZoneId;
        public float FrictionMultiplier = 0.6f;
        public float StaggerForce = 2f;
        public Transform FlowDirection;
        public DeckFrictionResponder FrictionResponder;

        public void ApplyFloodState(bool flooded)
        {
            if (FrictionResponder != null)
            {
                FrictionResponder.ApplyFlooding(flooded);
            }
        }

        public Vector3 GetFlowDirection()
        {
            return FlowDirection != null ? FlowDirection.forward : transform.forward;
        }
    }
}
