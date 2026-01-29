using NUnit.Framework;
using StormFishingVessel.Tasks;

namespace StormFishingVessel.Tests
{
    public class TaskValidationTests
    {
        [Test]
        public void TaskValidator_FindsMissingFields()
        {
            var tasks = new[]
            {
                new TaskDefinition { Id = "task_1", Title = "", VesselClass = "Crab" },
                new TaskDefinition { Id = "task_1", Title = "Duplicate", VesselClass = "" }
            };

            var errors = TaskValidator.Validate(tasks);
            Assert.IsTrue(errors.Count > 0);
        }

        [Test]
        public void TaskValidator_DetectsCycles()
        {
            var tasks = new[]
            {
                new TaskDefinition { Id = "a", Title = "A", VesselClass = "Crab", Prerequisites = new[] { "b" } },
                new TaskDefinition { Id = "b", Title = "B", VesselClass = "Crab", Prerequisites = new[] { "a" } }
            };

            var errors = TaskValidator.Validate(tasks);
            Assert.IsTrue(errors.Exists(e => e.Contains("cycles")));
        }
    }
}
