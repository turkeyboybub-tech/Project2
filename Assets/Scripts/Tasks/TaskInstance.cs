using System.Collections.Generic;

namespace StormFishingVessel.Tasks
{
    public class TaskInstance
    {
        public TaskDefinition Definition { get; }
        public TaskState State { get; set; }
        public float StartTime { get; set; }
        public float CompletionTime { get; set; }
        public readonly List<TaskInstance> Prerequisites = new List<TaskInstance>();

        public TaskInstance(TaskDefinition definition)
        {
            Definition = definition;
            State = TaskState.Available;
        }

        public void ResolvePrerequisites(Dictionary<string, TaskInstance> lookup)
        {
            Prerequisites.Clear();
            if (Definition.Prerequisites == null)
            {
                return;
            }

            foreach (var prerequisiteId in Definition.Prerequisites)
            {
                if (string.IsNullOrWhiteSpace(prerequisiteId))
                {
                    continue;
                }

                if (lookup.TryGetValue(prerequisiteId, out var instance))
                {
                    Prerequisites.Add(instance);
                }
            }

            if (Prerequisites.Count > 0)
            {
                State = TaskState.Locked;
            }
        }

        public bool CanStart()
        {
            foreach (var prerequisite in Prerequisites)
            {
                if (prerequisite.State != TaskState.Completed)
                {
                    return false;
                }
            }

            return State == TaskState.Available || State == TaskState.Locked;
        }
    }
}
