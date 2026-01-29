# Assumptions Log

Because the Unity scenes are not authored in this repository, the following assumptions are documented to ensure implementation remains faithful to the two authoritative documents:

1. **Geometry authoring**: Compartment layouts and access routes will be authored in Unity based on the Level Design Package diagrams, using the JSON compartment data as placement anchors.
2. **Lighting placement**: Deck flood amber and bridge red lighting are expressed through profile colors, with fixtures positioned in-scene to match the reference guide.
3. **Water visuals**: Green water on deck is represented as event-driven flood volumes; foam streaks and wave impact slams will be authored with URP particle and decal systems.
4. **Camera motion safety**: Rolling horizon is applied to the boat root, while any handheld shake is applied to a rig proxy, not the VR camera.
5. **Task phrasing**: Task descriptions remain high-level checks without procedural instructions, in line with safety guidance.
