# Testing ReadMe — Storm Survival Fishing Vessel VR

## Running Tests in Unity (Editor)
1. Open the Unity project (2022 LTS, URP).
2. Open **Test Runner** window.
3. Run EditMode tests:
   - `Assets/Tests/EditMode/TaskValidationTests.cs`
   - `Assets/Tests/EditMode/StormDirectorTests.cs`
4. Run PlayMode tests:
   - `Assets/Tests/PlayMode/BoatMotionComfortTests.cs`
   - `Assets/Tests/PlayMode/GreenWaterSystemTests.cs`
   - `Assets/Tests/PlayMode/DiegeticUITests.cs`

## Running Smoke Tests via Test Runner Scene
1. Use the provided `Assets/Scenes/TestRunner.unity` scene.
2. Add a GameObject with:
   - `TestRunnerController` (AutoRunSmokeTests enabled)
   - `PerformanceMetricsSampler`
   - `PerformanceTestMode` (EnablePerformanceTest optional)
3. Ensure QA reports are written to `Assets/_Game/Documentation/TestReports/` in Editor.

## Running on Quest
1. Enable Development Build.
2. Load `TestRunner` scene first in build settings.
3. Reports write to `Application.persistentDataPath` on device.

## Performance Test Mode
- Runs each level at intensity 3 and 5 equivalents.
- Captures min/avg/1% low FPS and memory snapshots.
- Outputs JSON report per run.

## Expected Thresholds
- FPS: min 72, avg >= 72, 1% low >= 60.
- GC allocations: near zero during normal gameplay.

## Interpreting Reports
- `perf_*.json`: performance metrics per run.
- `smoke_*.json`: smoke test results.

## Manual QA
Use `Assets/_Game/Documentation/QA_Checklist.md` for manual runs.
