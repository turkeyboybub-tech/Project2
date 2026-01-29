# Storm Survival Fishing Vessel VR (Unity 2022 LTS)

This repository scaffolds the VR storm survival fishing vessel experience for Meta Quest 3 (Unity 2022 LTS, URP, OpenXR, XR Interaction Toolkit). The implementation is grounded in the supplied design authorities:

- **Document A:** *Storm Survival Fishing Vessel Level Design Package*
- **Document B:** *Commercial Fishing Boat Storm Aesthetic Reference Guide*

All layout, vessel logic, compartment placement, storm escalation, and task concepts follow Document A, while lighting, VFX, audio layering, camera mood, and UI glow behaviors follow Document B.

## Build Order (Implemented Sequence)
1. XR bootstrap + comfort menu (see `Assets/Scripts/Comfort`).
2. Boat motion + comfort presets (see `Assets/Scripts/Boat`).
3. StormDirector + audio + weather (see `Assets/Scripts/Core`, `Assets/Scripts/Audio`, `Assets/Scripts/Weather`).
4. Diegetic UI instruments (see `Assets/Scripts/UI/DiegeticUI`).
5. Task system + task library (see `Assets/Scripts/Tasks`, `Assets/Resources/Tasks`).
6. Level 1 implementation (Crab Fishing Vessel) using `Assets/Resources/Configs/vessel_levels.json`.
7. Level 2 implementation (Longline Vessel) using `Assets/Resources/Configs/vessel_levels.json`.
8. Level 3 implementation (Factory Trawler) using `Assets/Resources/Configs/vessel_levels.json`.
9. Performance pass + documentation (this README).

## Systems Overview

### StormDirector
`StormDirector` and `StormProfile` drive storm intensity, wind, rain, fog, spindrift, and deck flood chances. Profiles are mapped to the three levels in `Assets/Resources/Configs/storm_profiles.json`.

### BoatMotionSystem
Applies roll/pitch/heave to the **boat root only**. Comfort scaling is applied via `ComfortController` to avoid direct camera shake while preserving the **CAMERA_ROLLING_HORIZON** vibe. Slam impulses are event-driven to match deck impact beats.

### GreenWaterSystem
Deck flood zones are modeled with `DeckFloodZone` and driven by `GreenWaterSystem` for **WATER_GREEN_WATER_ON_DECK** and **WATER_WAVE_IMPACT_SLAM** logic (solid water with friction reduction and player stagger hooks). Deck flood activation is keyed off storm stage deck flood chance.

### Weather & VFX
`WeatherSystem` mirrors the non-negotiable weather tags:
- **WEATHER_HEAVY_RAIN** and **WEATHER_SLEET** scale by storm stage.
- **WEATHER_FOG_THICK** is represented by fog density ramping.
- **WEATHER_SPINDRIFT** is driven by spindrift rate for crest sheets.

### AudioSystem
Layered exterior/interior audio sources with alarm and radio chatter for diegetic UI cues. Storm intensity cross-fades and amplifies strain.

### Diegetic UI
Radar, sonar ping, and analog gauges are implemented as diegetic components only:
- **UI_RADAR_GLOW** (`RadarDisplay`)
- **UI_SONAR_PING** (`SonarPingDisplay`)
- **UI_ANALOG_GAUGES** (`AnalogGaugeCluster`)

### Task System
`TaskSystem` loads a data-driven task library (`Assets/Resources/Tasks/task_library.json`, 114 tasks) with location-accurate tasks that scale with storm intensity. Tasks avoid real-world procedural steps and instead focus on high-level checks and confirmations.

## Level & Vessel Mapping
Vessel layouts and compartment placement use the JSON config in `Assets/Resources/Configs/vessel_levels.json`:

- **Level 1 – Crab Fishing Vessel** (day storm, violent motion, deck flooding, gear securing, navigation checks)
- **Level 2 – Longline Vessel** (twilight fog, radar-centric navigation, interior routing complexity)
- **Level 3 – Factory Trawler** (night freezing storm, ice accretion, multi-deck traversal)

## Visual & Mood Tags (Document B)
The following are explicitly represented via systems and data:

**Lighting**
- LIGHTING_DECK_FLOOD_AMBER (storm profile lighting colors)
- LIGHTING_BRIDGE_RED (storm profile lighting colors)
- LIGHTING_SPOTLIGHT_BEAM (spotlight cone angle in storm profiles)

**Weather**
- WEATHER_HEAVY_RAIN
- WEATHER_SLEET
- WEATHER_FOG_THICK
- WEATHER_SPINDRIFT

**Water**
- WATER_GREEN_WATER_ON_DECK
- WATER_WAVE_IMPACT_SLAM
- WATER_FOAM_STREAKS (intended for VFX implementation at waterline and deck flow)

**Materials**
- MATERIAL_WET_SHEEN
- MATERIAL_RUST_STREAKS
- MATERIAL_PEELING_PAINT
- MATERIAL_ICE_ENCASING

**Camera**
- CAMERA_WET_LENS (WeatherProfile wet lens chance hook)
- CAMERA_ROLLING_HORIZON (boat root motion)
- CAMERA_HANDHELD_SHAKE (applied to rig-mounted proxy, never direct VR camera)

**UI**
- UI_RADAR_GLOW
- UI_SONAR_PING
- UI_ANALOG_GAUGES

**Mood**
- MOOD_DREAD_STEADY
- MOOD_ADRENALINE_SPIKE
- MOOD_OMINOUS_LULL
- MOOD_EXHAUSTED_RELIEF

## Comfort & Safety
`ComfortSettings` and `ComfortController` support:
- Low/Medium/High motion scaling
- Optional vignette
- Optional horizon aid
- Wet-lens toggle
- Snap vs smooth turn (hook point for XR locomotion)

## Performance Considerations (Quest)
- Single-Pass Instanced ready (no per-eye camera scripts).
- Event-driven storm changes (no real fluid simulation).
- Keep deck flood volumes low-poly and spatially partitioned.
- Use baked light probes for interior zones and aggressive LODs on exterior props.

## Known Compromises
- Weather, audio, and VFX are stubbed with hook points and data mappings; final visual authoring should be done inside Unity with URP-compatible effects.
- Compartment geometry is represented as data; mesh layout must be authored to match the Level Design Package layout diagrams.

## Getting Started (Unity)
1. Create a new Unity 2022 LTS URP project.
2. Copy the `Assets` folder into the project.
3. Create ScriptableObject assets for StormProfile, WeatherProfile, BoatMotionPreset, and ComfortSettings.
4. Wire up the systems in a bootstrap scene in the build order listed above.
