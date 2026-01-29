using System.IO;
using UnityEngine;

namespace StormFishingVessel.QA
{
    public static class TestReportWriter
    {
        public static void WritePerformanceReport(PerformanceReport report)
        {
            var json = JsonUtility.ToJson(report, true);
            var fileName = $"perf_{report.RunName}_{System.DateTime.UtcNow:yyyyMMdd_HHmmss}.json";
            var path = GetReportPath(fileName);
            File.WriteAllText(path, json);
            Debug.Log($"Performance report written: {path}");
        }

        public static void WriteSmokeTestReport(SmokeTestReport report)
        {
            var json = JsonUtility.ToJson(report, true);
            var fileName = $"smoke_{report.RunName}_{System.DateTime.UtcNow:yyyyMMdd_HHmmss}.json";
            var path = GetReportPath(fileName);
            File.WriteAllText(path, json);
            Debug.Log($"Smoke test report written: {path}");
        }

        private static string GetReportPath(string fileName)
        {
            var basePath = Application.persistentDataPath;
#if UNITY_EDITOR
            basePath = Path.Combine(Application.dataPath, "_Game/Documentation/TestReports");
            Directory.CreateDirectory(basePath);
#endif
            return Path.Combine(basePath, fileName);
        }
    }

    [System.Serializable]
    public class SmokeTestReport
    {
        public string RunName;
        public string[] Results;
        public bool Passed;
    }
}
