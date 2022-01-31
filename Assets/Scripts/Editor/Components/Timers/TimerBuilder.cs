namespace Parasync.Editor.Components.Timers
{
    public class TimerBuilder
    {
        private float _duration = 1f;

        public TimerBuilder WithDuration(float duration)
        {
            _duration = duration;
            return this;
        }

        public Timer Build()
        {
            return new Timer(_duration);
        }

        public static implicit operator Timer(TimerBuilder builder)
        {
            return builder.Build();
        }
    }
}
