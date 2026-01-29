using UnityEngine;

namespace StormFishingVessel.Boat
{
    public class DeckFrictionResponder : MonoBehaviour
    {
        public PhysicMaterial DeckMaterial;
        public float DefaultFriction = 0.6f;
        public float FloodedFriction = 0.2f;

        private void Start()
        {
            if (DeckMaterial != null)
            {
                DefaultFriction = DeckMaterial.dynamicFriction;
            }
        }

        public void ApplyFlooding(bool flooded)
        {
            if (DeckMaterial == null)
            {
                return;
            }

            DeckMaterial.dynamicFriction = flooded ? FloodedFriction : DefaultFriction;
            DeckMaterial.staticFriction = flooded ? FloodedFriction : DefaultFriction;
        }
    }
}
