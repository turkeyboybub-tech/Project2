using System.Collections.Generic;
using System.Linq;

namespace StormFishingVessel.Tasks
{
    public static class TaskValidator
    {
        public static List<string> Validate(TaskDefinition[] tasks)
        {
            var errors = new List<string>();
            if (tasks == null)
            {
                errors.Add("Task list is null.");
                return errors;
            }

            var idSet = new HashSet<string>();
            foreach (var task in tasks)
            {
                if (task == null)
                {
                    errors.Add("Task definition is null.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(task.Id))
                {
                    errors.Add("Task missing id.");
                }
                else if (!idSet.Add(task.Id))
                {
                    errors.Add($"Duplicate task id: {task.Id}");
                }

                if (string.IsNullOrWhiteSpace(task.Title))
                {
                    errors.Add($"Task {task.Id} missing title.");
                }

                if (string.IsNullOrWhiteSpace(task.VesselClass))
                {
                    errors.Add($"Task {task.Id} missing vessel class.");
                }
            }

            var taskMap = tasks.Where(t => t != null && !string.IsNullOrWhiteSpace(t.Id))
                .ToDictionary(t => t.Id, t => t);

            foreach (var task in tasks)
            {
                if (task == null || task.Prerequisites == null)
                {
                    continue;
                }

                foreach (var prerequisite in task.Prerequisites)
                {
                    if (string.IsNullOrWhiteSpace(prerequisite))
                    {
                        continue;
                    }

                    if (!taskMap.ContainsKey(prerequisite))
                    {
                        errors.Add($"Task {task.Id} references missing prerequisite {prerequisite}.");
                    }
                }
            }

            if (HasCycles(tasks))
            {
                errors.Add("Task dependency graph contains cycles.");
            }

            return errors;
        }

        private static bool HasCycles(TaskDefinition[] tasks)
        {
            var graph = new Dictionary<string, List<string>>();
            foreach (var task in tasks)
            {
                if (task == null || string.IsNullOrWhiteSpace(task.Id))
                {
                    continue;
                }

                if (!graph.ContainsKey(task.Id))
                {
                    graph[task.Id] = new List<string>();
                }

                if (task.Prerequisites == null)
                {
                    continue;
                }

                foreach (var prerequisite in task.Prerequisites)
                {
                    if (!string.IsNullOrWhiteSpace(prerequisite))
                    {
                        graph[task.Id].Add(prerequisite);
                    }
                }
            }

            var visiting = new HashSet<string>();
            var visited = new HashSet<string>();

            foreach (var node in graph.Keys)
            {
                if (DetectCycle(node, graph, visiting, visited))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool DetectCycle(string node, Dictionary<string, List<string>> graph, HashSet<string> visiting, HashSet<string> visited)
        {
            if (visited.Contains(node))
            {
                return false;
            }

            if (visiting.Contains(node))
            {
                return true;
            }

            visiting.Add(node);
            if (graph.TryGetValue(node, out var neighbors))
            {
                foreach (var neighbor in neighbors)
                {
                    if (graph.ContainsKey(neighbor) && DetectCycle(neighbor, graph, visiting, visited))
                    {
                        return true;
                    }
                }
            }

            visiting.Remove(node);
            visited.Add(node);
            return false;
        }
    }
}
