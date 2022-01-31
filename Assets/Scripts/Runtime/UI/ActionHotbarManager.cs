using System;
using UnityEngine;
using UnityEngine.Events;

namespace Parasync.Runtime.UI
{
    public class ActionHotbarManager : MonoBehaviour
    {
        [SerializeField] private ActionSlot[] player1Slots;
        [SerializeField] private ActionSlot[] player2Slots;

        [Header("Event Listeners")]
        [SerializeField] private UnityEvent onTurnEnd;
        [SerializeField] private UnityEvent onActionsCombined;
        [SerializeField] private UnityEvent onActionsFailedToBeCombined;

        private float _durationPerIteration;
        private int _totalIterationsPerTurn;
        private int _currActionSlot;

        private void Awake()
        {
            _currActionSlot = 0;
        }

        public void OnTimerSetup(float duration, int iterations)
        {
            _durationPerIteration = duration;
            _totalIterationsPerTurn = iterations;

            for (int i = 0; i < iterations; i++)
            {
                player1Slots[i].gameObject.SetActive(true);
                player2Slots[i].gameObject.SetActive(true);
            }
        }

        public void OnTick(float remainingSeconds)
        {
            if (_currActionSlot >= _totalIterationsPerTurn) return;

            float progressFilled = (_durationPerIteration - remainingSeconds) / _durationPerIteration;

            player1Slots[_currActionSlot].OnTick(progressFilled);
            player2Slots[_currActionSlot].OnTick(progressFilled);
        }

        public void OnIterationEnd(Action resetTimer)
        {
            if (_currActionSlot++ == _totalIterationsPerTurn - 1)
                HandleTurnEnd();

            if (_currActionSlot < _totalIterationsPerTurn)
                resetTimer();
        }

        private void HandleTurnEnd() => onTurnEnd?.Invoke();

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

            player1Slots[index].TweenResetProgress();
            player2Slots[index].TweenResetProgress();

            onActionsCombined?.Invoke();
        }

        public void OnActionsFailedToCombine(int index)
        {
            player1Slots[index].TweenFadeArrow();
            player2Slots[index].TweenFadeArrow();

            player1Slots[index].TweenResetProgress();
            player2Slots[index].TweenResetProgress();

            onActionsFailedToBeCombined?.Invoke();
        }

        public void OnEnemyTurnEnd() => _currActionSlot = 0;
    }
}
