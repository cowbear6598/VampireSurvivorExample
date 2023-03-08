using TimeSystem;

namespace Tests.Runtime
{
    public class FakeTimeService : ITimeService
    {
        public float GetDeltaTime() => 1;
    }
}