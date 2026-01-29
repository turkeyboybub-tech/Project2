namespace StormFishingVessel.Tasks
{
    public interface ITaskCueEmitter
    {
        void EmitTaskStarted(TaskInstance instance);
        void EmitTaskCompleted(TaskInstance instance);
        void EmitTaskFailed(TaskInstance instance);
    }
}
