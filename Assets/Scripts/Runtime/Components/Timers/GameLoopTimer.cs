using UnityEngine;
using UnityEngine.Events;

namespace Parasync.Runtime.Components.Timers
{
    public class GameLoopTimer : MonoBehaviour
    {
        [SerializeField] private int totalIterationsPerTurn = 2;
        [Tooltip("Duration in seconds")]
        [SerializeField] private float durationPerIteration = 3f;
        [SerializeField] private UnityEvent<float> onStart = null;
        [SerializeField] private UnityEvent<float> onTick = null;
        [SerializeField] private UnityEvent<int> onIterationEnd = null;
        [SerializeField] private UnityEvent onTurnEnd = null;

        private Timer _timer;
        private int _currIterations;

        private void Start()
        {
            _timer = new Timer(durationPerIteration);
            _currIterations = totalIterationsPerTurn;

            // Notify when game loop has started
            onStart?.Invoke(durationPerIteration);

            // Subscribe to notify per tick
            _timer.OnTick += () => onTick?.Invoke(_timer.RemainingSeconds);

            // Subscribe to handle events per iteration
            _timer.OnTimerEnd += HandleIterationEnd;
        }

        private void Update() => _timer.Tick(Time.deltaTime);

        private void HandleIterationEnd()
        {
            // Alert listeners that timer has ended (iteration)
            onIterationEnd?.Invoke(_currIterations);

            // If iterations reach 1, we have completed both iteration and turn
            if (_currIterations-- == 1) HandleTurnEnd();

            // Reset timer as long as there are iterations
            if (_currIterations > 0) _timer.ResetTimer();
        }

        private void HandleTurnEnd()
        {
            // Alert listeners that turn has ended (all iterations completed)
            onTurnEnd?.Invoke();
        }

        private void StartNextTurn()
        {
            // Reset iteration amount for next turn
            _currIterations = totalIterationsPerTurn;
        }
    }
}
