using FluentAssertions;
using NUnit.Framework;
using Parasync.Editor;
using Parasync.Editor.Components.Timers;

namespace Parasync.Tests.Editor.Components.Timers
{
    public class TimerTests
    {
        [Test]
        [TestCase(1f)]
        [TestCase(3f)]
        [TestCase(14.6f)]
        public void TimerStartingDurationIsSet(float duration)
        {
            Timer timer = A.Timer.WithDuration(duration);

            timer.RemainingSeconds.Should().Be(duration);
        }

        [Test]
        public void TimerTickingBelowZeroSeconds_StopsAtZero()
        {
            Timer timer = A.Timer.WithDuration(1f);

            timer.Tick(2f);

            timer.RemainingSeconds.Should().Be(0f);
        }

        [Test]
        public void TimerEnds_EventIsRaised()
        {
            Timer timer = A.Timer.WithDuration(1f);
            bool eventHasBeenRaised = false;

            timer.OnTimerEnd += () => eventHasBeenRaised = true;
            timer.Tick(1f);

            eventHasBeenRaised.Should().BeTrue();
        }

        [Test]
        public void TimerDoesNotEnd_EventIsNotRaised()
        {
            Timer timer = A.Timer.WithDuration(1f);
            bool eventHasBeenRaised = false;

            timer.OnTimerEnd += () => eventHasBeenRaised = true;
            timer.Tick(0.5f);

            eventHasBeenRaised.Should().BeFalse();
        }
    }
}
