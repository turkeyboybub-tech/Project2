# QA Checklist — Storm Survival Fishing Vessel VR

## Smoke Tests
### Smoke Test 1: Boot & Menu
- Game launches to menu on Quest.
- Start Level Select works.
- Settings/Comfort changes persist after restart.

### Smoke Test 2: Level 1 Completion (Crab Fishing Vessel)
- Player spawns correctly on deck/bridge.
- First 3 tasks can be completed.
- One green water event triggers and resolves.
- Win condition reachable and triggers “Level Complete.”

### Smoke Test 3: Level 2 Fog + Radar Reliance
- Fog density present (reduced visibility).
- Radar is functional and readable (UI_RADAR_GLOW, UI_SONAR_PING).
- Spotlight beam aids visibility if implemented.
- Task chain reaches navigation milestone.

### Smoke Test 4: Level 3 Freezing/Sleet + Icing
- Sleet/icing visuals present (MATERIAL_ICE_ENCASING).
- Icing hazard affects traversal or task constraint.
- Stability/icing warning triggers diegetically.
- Level can still be completed.

### Smoke Test 5: Fail States
- Trigger a fail state (flood threshold or critical power loss).
- Fail flow is non-graphic and recoverable.
- Restart works.

### Smoke Test 6: No HUD Rule
- No floating HUD canvases.
- All objectives delivered via diegetic devices (checklist, radio, instruments).

## Regression Checklist
- Task chain never soft-locks (prerequisites valid, interactables reachable).
- Green water never leaves deck friction permanently altered.
- Radar/gauges readable under fog/rain.
- Storm escalation progresses within expected pacing.
- Performance remains above 72 FPS in standard settings.

## Comfort & Nausea Guardrails
- Comfort presets Low/Medium/High scale boat motion amplitude.
- Vignette toggle works; UI remains readable.
- Wet lens toggle works.
- Snap turn settings apply properly.
- No high-frequency shake applied to VR camera.
- Horizon roll comes from boat/world motion only.

## Diegetic UI Compliance
- UI_RADAR_GLOW visible on radar displays.
- UI_SONAR_PING audible/visible.
- UI_ANALOG_GAUGES update with system state.
- No non-diegetic HUD elements active.

## Storm Aesthetic Compliance (Document B)
- LIGHTING_DECK_FLOOD_AMBER and LIGHTING_BRIDGE_RED present.
- WEATHER_HEAVY_RAIN, WEATHER_SLEET, WEATHER_FOG_THICK, WEATHER_SPINDRIFT match intensity scaling.
- WATER_GREEN_WATER_ON_DECK and WATER_WAVE_IMPACT_SLAM events visible.
- MATERIAL_WET_SHEEN, MATERIAL_RUST_STREAKS, MATERIAL_PEELING_PAINT, MATERIAL_ICE_ENCASING applied where appropriate.
- CAMERA_WET_LENS, CAMERA_ROLLING_HORIZON, CAMERA_HANDHELD_SHAKE (rig-only) present.
- Mood pacing hits MOOD_DREAD_STEADY, MOOD_ADRENALINE_SPIKE, MOOD_OMINOUS_LULL, MOOD_EXHAUSTED_RELIEF.

## Known Risks
- Fog + rain can obscure diegetic UI readability.
- Performance drops during heavy spindrift and green water events.
- Task chain can block if prerequisite hooks misconfigured.
