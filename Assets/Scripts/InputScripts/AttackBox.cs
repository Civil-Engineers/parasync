using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AttackBox : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right };
    // Start is called before the first frame update
    [SerializeField] private RectTransform image;

    [SerializeField] private RectTransform center;

    public bool flipped = false;

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

    public void Move()
    {
        image.DOAnchorPosX(0, 1);
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

    public void StartQueue()
    {

    }

    public void Hide()
    {

    }
}
