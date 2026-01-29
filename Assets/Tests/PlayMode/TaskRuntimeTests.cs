using System.Collections;
using NUnit.Framework;
using StormFishingVessel.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

namespace StormFishingVessel.Tests
{
    public class TaskRuntimeTests
    {
        [UnityTest]
        public IEnumerator TaskSystem_AllowsStartAndComplete()
        {
            var taskA = new TaskDefinition { Id = "task_a", Title = "A", VesselClass = "Crab" };
            var taskB = new TaskDefinition { Id = "task_b", Title = "B", VesselClass = "Crab", Prerequisites = new[] { "task_a" } };

            var wrapper = new TaskWrapper { Tasks = new[] { taskA, taskB } };
            var json = JsonUtility.ToJson(wrapper, true);
            var textAsset = new TextAsset(json);

            var library = ScriptableObject.CreateInstance<TaskLibrary>();
            library.TaskJson = textAsset;

            var go = new GameObject("TaskSystem");
            var system = go.AddComponent<TaskSystem>();
            system.Library = library;
            system.CurrentVesselClass = "Crab";
            system.LoadTasks();

            var startA = system.StartTask("task_a");
            Assert.AreEqual(TaskState.InProgress, startA.State);

            var completeA = system.CompleteTask("task_a");
            Assert.AreEqual(TaskState.Completed, completeA.State);

            var startB = system.StartTask("task_b");
            Assert.AreEqual(TaskState.InProgress, startB.State);

            Object.Destroy(go);
            yield return null;
        }

        [System.Serializable]
        private class TaskWrapper
        {
            public TaskDefinition[] Tasks;
        }
    }
}
