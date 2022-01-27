using UnityEngine;
using UnityEngine.Events;
using Parasync.Runtime.Components.Timers;
using Parasync.Runtime.Actions.Movement;

namespace Parasync.Runtime.UI
{
    public class ActionHotbarManager : MonoBehaviour
    {
        [SerializeField] private ActionSlot[] player1Slots;
        [SerializeField] private ActionSlot[] player2Slots;
        [Tooltip("Duration in seconds")]
        [SerializeField] private float durationPerIteration = 3f;

        [Header("Event Listeners")]
        [SerializeField] private UnityEvent onIterationEnd;
        [SerializeField] private UnityEvent onTurnEnd;
        [SerializeField] private UnityEvent onActionsCombined;
        [SerializeField] private UnityEvent onActionsFailedToBeCombined;

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
            onIterationEnd?.Invoke();

            if (_currActionSlot++ == _totalIterationsPerTurn - 1)
                HandleTurnEnd();

            if (_currActionSlot < _totalIterationsPerTurn)
                _timer?.ResetTimer();
        }

        private void HandleTurnEnd()
        {
            onTurnEnd?.Invoke();
        }

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

        public void OnPlayerMove(int playerNum, Vector2 direction)
        {
            switch (playerNum)
            {
                case 1:
                    player1Slots[_currActionSlot].TweenRotateArrow(direction);
                    break;
                case 2:
                    player2Slots[_currActionSlot].TweenRotateArrow(direction);
                    break;
            }
        }

        public void OnActionsCombine(int index, Vector2 direction)
        {
            player1Slots[index].TweenCombineArrows(direction);
            player2Slots[index].TweenCombineArrows(direction);

            onActionsCombined?.Invoke();
        }

        public void OnActionsFailedToCombine(int index)
        {
            player1Slots[index].TweenFadeArrow();
            player2Slots[index].TweenFadeArrow();

            onActionsFailedToBeCombined?.Invoke();
        }
    }
}
