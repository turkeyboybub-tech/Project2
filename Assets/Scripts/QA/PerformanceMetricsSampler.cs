using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace StormFishingVessel.QA
{
    public class PerformanceMetricsSampler : MonoBehaviour
    {
        public bool IsSampling { get; private set; }
        public float SampleInterval = 0.5f;

        private float _timer;
        private readonly List<float> _frameTimes = new List<float>();
        private readonly List<long> _memorySamples = new List<long>();

        public void StartSampling()
        {
            _frameTimes.Clear();
            _memorySamples.Clear();
            _timer = 0f;
            IsSampling = true;
        }

        public void StopSampling()
        {
            IsSampling = false;
        }

        private void Update()
        {
            if (!IsSampling)
            {
                return;
            }

            _timer += Time.unscaledDeltaTime;
            if (_timer >= SampleInterval)
            {
                _timer = 0f;
                _frameTimes.Add(Time.unscaledDeltaTime);
                _memorySamples.Add(Profiler.GetTotalAllocatedMemoryLong());
            }
        }

        public PerformanceReport BuildReport(string runName)
        {
            var report = new PerformanceReport
            {
                RunName = runName,
                MinFps = CalculateMinFps(),
                AvgFps = CalculateAverageFps(),
                OnePercentLowFps = CalculateOnePercentLowFps(),
                MemorySamples = _memorySamples.ToArray()
            };

            return report;
        }

        private float CalculateMinFps()
        {
            var maxFrameTime = 0f;
            foreach (var frameTime in _frameTimes)
            {
                if (frameTime > maxFrameTime)
                {
                    maxFrameTime = frameTime;
                }
            }

            return maxFrameTime > 0f ? 1f / maxFrameTime : 0f;
        }

        private float CalculateAverageFps()
        {
            if (_frameTimes.Count == 0)
            {
                return 0f;
            }

            var total = 0f;
            foreach (var frameTime in _frameTimes)
            {
                total += frameTime;
            }

            var avg = total / _frameTimes.Count;
            return avg > 0f ? 1f / avg : 0f;
        }

        private float CalculateOnePercentLowFps()
        {
            if (_frameTimes.Count == 0)
            {
                return 0f;
            }

            var sorted = new List<float>(_frameTimes);
            sorted.Sort();
            var index = Mathf.Clamp(Mathf.CeilToInt(sorted.Count * 0.99f) - 1, 0, sorted.Count - 1);
            var frameTime = sorted[index];
            return frameTime > 0f ? 1f / frameTime : 0f;
        }
    }

    [System.Serializable]
    public class PerformanceReport
    {
        public string RunName;
        public float MinFps;
        public float AvgFps;
        public float OnePercentLowFps;
        public long[] MemorySamples;
    }
}
