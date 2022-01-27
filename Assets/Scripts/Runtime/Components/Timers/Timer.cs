using System;

namespace Parasync.Runtime.Components.Timers
{
    public class Timer
    {
        public float Duration { get; private set; }

        public float RemainingSeconds { get; private set; }

        public Timer(float duration)
        {
            Duration = duration;
            RemainingSeconds = duration;
        }

        public event Action OnTick;
        public event Action OnTimerEnd;

        public void Tick(float deltaTime)
        {
            // Stop ticking if timer has ended
            if (RemainingSeconds == 0f) return;

            // Tick timer down by the time it took to complete last frame
            RemainingSeconds -= deltaTime;
            OnTick?.Invoke();

            CheckForTimerEnd();
        }

        private void CheckForTimerEnd()
        {
            // Leave if there is still time remaining
            if (RemainingSeconds > 0f) return;

            // Set to zero, deltaTime subtraction may cause it to go below zero
            RemainingSeconds = 0f;

            // Alert any listeners that timer has ended
            OnTimerEnd?.Invoke();
        }

        public void ResetTimer() => RemainingSeconds = Duration;
    }
}
