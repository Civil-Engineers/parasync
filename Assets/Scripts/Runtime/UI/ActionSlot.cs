using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Parasync.Runtime.UI
{
    public class ActionSlot : MonoBehaviour
    {
        [SerializeField] private Image progressBar;
        [SerializeField] private RectTransform arrow;
        [SerializeField] private RectTransform center;

        private Vector2 _arrowAnchorPos;

        private void Awake()
        {
            _arrowAnchorPos = arrow.anchoredPosition;
            arrow.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        public void OnTick(float progressFilled)
        {
            progressBar.fillAmount = progressFilled;
        }

        public void TweenResetProgress()
        {
            progressBar.DOFillAmount(0, 0.7f);
        }

        public void TweenRotateArrow(Vector2 direction)
        {
            arrow.DORotate(new Vector3(0, 0, GetArrowAngle(direction)), 0.2f);
            arrow.anchoredPosition = _arrowAnchorPos;
            arrow.localScale = new Vector3(1, 1, 1);
            arrow.GetComponent<Image>().DOFade(1, 0.2f);
        }

        public void TweenCombineArrows(Vector2 direction)
        {
            arrow.DOAnchorPosX(0, 1);
            arrow.DORotate(new Vector3(0, 0, GetCombinedArrowAngle(direction)), 0.5f).SetDelay(0.5f);

            arrow.DOMove(center.position, 0.5f).SetDelay(0.5f);
            arrow.DOScale(new Vector3(2, 2, 2), 0.1f).SetDelay(0.8f);
            arrow.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.1f).SetDelay(0.9f);
            arrow.GetComponent<Image>().DOFade(0, 0.1f).SetDelay(0.9f);
        }

        public void TweenFadeArrow()
        {
            Vector3 currPos = arrow.position;
            arrow.DOMove(currPos + new Vector3(0, -100, 0), 0.5f).SetDelay(0.5f);
            arrow.GetComponent<Image>().DOFade(0, 0.1f).SetDelay(0.9f);
        }

        private float GetArrowAngle(Vector2 direction)
        {
            float angle = 0;

            if (direction.x == 0 && direction.y == 1)
                angle = 0;
            else if (direction.x == 0 && direction.y == -1)
                angle = 180;
            else if (direction.x == -1 && direction.y == 0)
                angle = 90;
            else if (direction.x == 1 && direction.y == 0)
                angle = -90;

            return angle;
        }

        private float GetCombinedArrowAngle(Vector2 direction)
        {
            float angle = 0;

            // Up
            if (direction.x == 0 && direction.y >= 1)
                angle = 0;
            // Up Right
            else if (direction.x >= 1 && direction.y >= 1)
                angle = -45;
            // Right
            else if (direction.x >= 1 && direction.y == 0)
                angle = -90;
            // Down Right
            else if (direction.x == 1 && direction.y == -1)
                angle = -135;
            // Down
            else if (direction.x == 0 && direction.y <= -1)
                angle = -180;
            // Down Left
            else if (direction.x <= -1 && direction.y <= -1)
                angle = -225;
            // Left
            else if (direction.x <= -1 && direction.y == 0)
                angle = -270;
            // Up left
            else if (direction.x <= -1 && direction.y >= 1)
                angle = -315;

            return angle;
        }
    }
}
