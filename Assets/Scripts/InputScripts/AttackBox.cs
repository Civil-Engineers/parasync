using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        image.GetComponent<Image>().color = new Color(1,1,1,0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(Direction direction)
    {
        float angle = 0;
        switch (direction)
        {
            case Direction.Up:
                angle = 0;
                break;
            case Direction.UpRight:
                angle = -45;
                break;
            case Direction.Right:
                angle = -90;
                break;
            case Direction.DownRight:
                angle = -135;
                break;
            case Direction.Down:
                angle = -180;
                break;
            case Direction.DownLeft:
                angle = -225;
                break;
            case Direction.Left:
                angle = -270;
                break;
            case Direction.UpLeft:
                angle = -315;
                break;
        }
        image.DOAnchorPosX(0, 1);
        image.DORotate(new Vector3(0, 0, angle), .25f).SetDelay(.75f);
    }

    public void Register(Direction direction)
    {
        float angle = 0;
        switch (direction)
        {
            case Direction.Up:
                angle = 0;
                break;
            case Direction.Down:
                angle = 180;
                break;
            case Direction.Left:
                angle = 90;
                break;
            case Direction.Right:
                angle = -90;
                break;
        }
        image.DORotate(new Vector3(0, 0, angle), .2f);
        image.anchoredPosition = imageInitLoc;
        image.GetComponent<Image>().DOFade(1, .2f);
    }

    public void StartQueue(float staringTime)
    {
        progressBg.fillAmount = 0;
        progressBg.DOFillAmount(1, staringTime);
    }

    public void Hide()
    {

    }
}
