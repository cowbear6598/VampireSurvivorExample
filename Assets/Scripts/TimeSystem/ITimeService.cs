using UnityEngine;

namespace TimeSystem
{
    public interface ITimeService
    {
        float GetDeltaTime();
    }

    public class TimeService : ITimeService
    {
        public float GetDeltaTime()
        {
            return Time.deltaTime;
        }
    }
}