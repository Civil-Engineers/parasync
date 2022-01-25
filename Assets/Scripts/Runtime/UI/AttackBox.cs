using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Parasync.Runtime.UI
{
    public class AttackBox : MonoBehaviour
    {
        public enum Direction { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft };
        // Start is called before the first frame update
        [SerializeField] private RectTransform image;

        [SerializeField] private RectTransform center;
        [SerializeField] private Image progressBg;

        Vector2 imageInitLoc;

        void Start()
        {
            imageInitLoc = image.anchoredPosition;
            image.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        public void Move(Vector2 movement)
        {
            float angle = 0;

            // Up
            if (movement.x == 0 && movement.y >= 1)
                angle = 0;
            // Up Right
            else if (movement.x >= 1 && movement.y >= 1)
                angle = -45;
            // Right
            else if (movement.x >= 1 && movement.y == 0)
                angle = -90;
            // Down Right
            else if (movement.x == 1 && movement.y == -1)
                angle = -135;
            // Down
            else if (movement.x == 0 && movement.y <= -1)
                angle = -180;
            // Down Left
            else if (movement.x <= -1 && movement.y <= -1)
                angle = -225;
            // Left
            else if (movement.x <= -1 && movement.y == 0)
                angle = -270;
            // Up left
            else if (movement.x <= -1 && movement.y >= 1)
                angle = -315;

            image.DOAnchorPosX(0, 1);
            image.DORotate(new Vector3(0, 0, angle), .5f).SetDelay(.5f);

            image.DOScale(new Vector3(2f, 2f, 2f), .1f).SetDelay(.8f);
            image.DOScale(new Vector3(.5f, .5f, .5f), .1f).SetDelay(.9f);
            image.GetComponent<Image>().DOFade(0, .1f).SetDelay(.9f);
        }

        public void Register(Vector2 direction)
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

            image.DORotate(new Vector3(0, 0, angle), .2f);
            image.anchoredPosition = imageInitLoc;
            image.localScale = new Vector3(1, 1, 1);
            image.GetComponent<Image>().DOFade(1, .2f);
        }

        public void StartQueue(float staringTime)
        {
            progressBg.fillAmount = 0;
            progressBg.DOFillAmount(1, staringTime);
        }

        public void Fade()
        {
            image.GetComponent<Image>().DOFade(0, .2f);
        }
    }
}
