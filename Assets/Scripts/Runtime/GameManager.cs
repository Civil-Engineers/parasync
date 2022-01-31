using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Parasync.Editor;
using Parasync.Editor.Components.Timers;

namespace Parasync.Runtime
{
    public class GameManager : MonoBehaviour
    {
        [Tooltip("Duration in seconds")]
        [SerializeField] private float durationPerIteration = 3f;
        [Range(1, 5)]
        [Tooltip("Number of times the timer will repeat before turn ends")]
        [SerializeField] private int iterationsPerTurn = 5;

        [Header("Event Listeners")]
        [SerializeField] private UnityEvent<float, int> onTimerSetup;
        [SerializeField] private UnityEvent<float> onTick;
        [SerializeField] private UnityEvent<Action> onIterationEnd;

        private Timer _timer;

        private void Awake()
        {
            _timer = A.Timer.WithDuration(durationPerIteration);

            _timer.OnTick += HandleTick;
            _timer.OnTimerEnd += HandleIterationEnd;

            onTimerSetup?.Invoke(durationPerIteration, iterationsPerTurn);
        }

        private void Update() => _timer.Tick(Time.deltaTime);

        private void OnDestroy()
        {
            _timer.OnTick -= HandleTick;
            _timer.OnTimerEnd -= HandleIterationEnd;
        }

        private void HandleTick() => onTick?.Invoke(_timer.RemainingSeconds);

        private void HandleIterationEnd() => onIterationEnd?.Invoke(_timer.ResetTimer);

        public void OnEnemyTurnEnd() => _timer.ResetTimer();
    }
}
