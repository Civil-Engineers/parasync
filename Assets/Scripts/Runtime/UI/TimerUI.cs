using UnityEngine;
using UnityEngine.UI;

namespace Parasync.Runtime.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private Image timerImage;

        private float _maxTime;

        public void OnGameLoopStart(float duration) => _maxTime = duration;

        public void OnTick(float remainingTime)
        {
            timerImage.fillAmount = remainingTime / _maxTime;
        }

        public void OnIterationEnd(int iteration)
        {
            Debug.Log("Iteration ended: " + iteration);
        }

        public void OnTurnEnd()
        {
            Debug.Log("Turn ended");
        }
    }
}
