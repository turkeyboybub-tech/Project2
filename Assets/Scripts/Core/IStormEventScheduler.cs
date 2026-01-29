namespace StormFishingVessel.Core
{
    public interface IStormEventScheduler
    {
        void ScheduleEvent(string eventId, float delaySeconds);
        void CancelEvent(string eventId);
    }
}
