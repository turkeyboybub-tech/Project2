using System.Collections.Generic;
using UnityEngine;

namespace StormFishingVessel.Tasks
{
    public class TaskSystem : MonoBehaviour
    {
        public TaskLibrary Library;
        public string CurrentVesselClass;
        public List<TaskDefinition> ActiveTasks = new List<TaskDefinition>();

        public delegate void TaskStateChanged(TaskInstance instance);
        public event TaskStateChanged OnTaskStateChanged;

        private TaskDefinition[] _allTasks;
        private readonly Dictionary<string, TaskInstance> _instances = new Dictionary<string, TaskInstance>();

        private void Start()
        {
            LoadTasks();
            SelectInitialTasks();
        }

        public void LoadTasks()
        {
            if (Library == null || Library.TaskJson == null)
            {
                return;
            }

            var wrapper = JsonUtility.FromJson<TaskWrapper>(Library.TaskJson.text);
            _allTasks = wrapper != null ? wrapper.Tasks : new TaskDefinition[0];
            BuildInstances();
        }

        public void SelectInitialTasks()
        {
            ActiveTasks.Clear();
            if (_allTasks == null)
            {
                return;
            }

            foreach (var task in _allTasks)
            {
                if (task.VesselClass == CurrentVesselClass)
                {
                    ActiveTasks.Add(task);
                    if (ActiveTasks.Count >= 6)
                    {
                        break;
                    }
                }
            }
        }

        public TaskInstance StartTask(string taskId)
        {
            if (!_instances.TryGetValue(taskId, out var instance))
            {
                return null;
            }

            if (!instance.CanStart())
            {
                return instance;
            }

            instance.State = TaskState.InProgress;
            instance.StartTime = Time.time;
            OnTaskStateChanged?.Invoke(instance);
            return instance;
        }

        public TaskInstance CompleteTask(string taskId)
        {
            if (!_instances.TryGetValue(taskId, out var instance))
            {
                return null;
            }

            if (instance.State != TaskState.InProgress)
            {
                return instance;
            }

            instance.State = TaskState.Completed;
            instance.CompletionTime = Time.time;
            OnTaskStateChanged?.Invoke(instance);
            return instance;
        }

        public TaskInstance FailTask(string taskId)
        {
            if (!_instances.TryGetValue(taskId, out var instance))
            {
                return null;
            }

            if (instance.State != TaskState.InProgress)
            {
                return instance;
            }

            instance.State = TaskState.Failed;
            instance.CompletionTime = Time.time;
            OnTaskStateChanged?.Invoke(instance);
            return instance;
        }

        private void BuildInstances()
        {
            _instances.Clear();
            if (_allTasks == null)
            {
                return;
            }

            foreach (var task in _allTasks)
            {
                if (string.IsNullOrWhiteSpace(task.Id))
                {
                    continue;
                }

                if (!_instances.ContainsKey(task.Id))
                {
                    _instances.Add(task.Id, new TaskInstance(task));
                }
            }

            foreach (var instance in _instances.Values)
            {
                instance.ResolvePrerequisites(_instances);
            }
        }

        [System.Serializable]
        private class TaskWrapper
        {
            public TaskDefinition[] Tasks;
        }
    }
}
