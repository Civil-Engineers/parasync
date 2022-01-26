using UnityEngine;
using UnityEngine.Events;
using Parasync.Runtime.Components.Timers;

namespace Parasync.Runtime.UI
{
    public class ActionHotbarManager : MonoBehaviour
    {
        [SerializeField] private ActionSlot[] player1Slots;
        [SerializeField] private ActionSlot[] player2Slots;
        [Tooltip("Duration in seconds")]
        [SerializeField] private float durationPerIteration = 3f;

        [Header("Event Listeners")]
        [SerializeField] private UnityEvent<int> onSlotChange;
        [SerializeField] private UnityEvent onTurnEnd;

        private Timer _timer;
        private int _totalIterationsPerTurn;
        private int _currActionSlot;

        private void Awake()
        {
            _timer = new Timer(durationPerIteration);
            _totalIterationsPerTurn = player1Slots.Length;
            _currActionSlot = 0;

            _timer.OnTick += HandleTick;
            _timer.OnTimerEnd += HandleIterationEnd;
        }

        private void Update() => _timer.Tick(Time.deltaTime);

        private void OnDestroy()
        {
            _timer.OnTick -= HandleTick;
            _timer.OnTimerEnd -= HandleIterationEnd;
        }

        private void HandleTick()
        {
            if (_currActionSlot >= _totalIterationsPerTurn) return;

            float remainingTime = _timer.RemainingSeconds;
            float progressFilled = (durationPerIteration - remainingTime) / durationPerIteration; 
           
            player1Slots[_currActionSlot].OnTick(progressFilled);
            player2Slots[_currActionSlot].OnTick(progressFilled);
        }

        private void HandleIterationEnd()
        {
            if (_currActionSlot++ == _totalIterationsPerTurn - 1)
                HandleTurnEnd();

            if (_currActionSlot < _totalIterationsPerTurn)
                _timer?.ResetTimer();
        }

        private void HandleTurnEnd() => onTurnEnd?.Invoke();

        public void Restart()
        {
            for (int i = 0; i < _totalIterationsPerTurn; i++)
            {
                player1Slots[i].ResetProgress();
                player2Slots[i].ResetProgress();
            }

            _currActionSlot = 0;
            _timer?.ResetTimer();
        }
    }
}
