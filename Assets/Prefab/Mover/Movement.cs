using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Movement : MonoBehaviour
{

    [SerializeField] private float speed = 5;

    private Vector3 position = new Vector3(0, 0, 0);
    private bool right = true;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    private void Update()
    {

    }

    public void OnUp()
    {
        position += new Vector3(0, 0, speed);
        Tween(true);
    }
    public void OnDown()
    {
        position += new Vector3(0, 0, -speed);
        Tween(false);
    }
    public void OnLeft()
    {
        position += new Vector3(-speed, 0, 0);

        Tween(false);
    }
    public void OnRight()
    {
        position += new Vector3(speed, 0, 0);
        Tween(true);
    }
    private void Tween(bool toRight)
    {
        float time = .2f;
        if (!right && toRight) {
            right = true;
            transform.DORotate(new Vector3(0, -90, 0), time).SetEase(Ease.InQuad);
            transform.DOScale(new Vector3(1, 1, 1), 0).SetDelay(time);
            transform.DORotate(new Vector3(0, 90, 0), 0).SetDelay(time).SetEase(Ease.OutQuad);
            transform.DORotate(new Vector3(0, 0, 0), time).SetDelay(time);
        } else if(right && !toRight) {
            right = false;
            transform.DORotate(new Vector3(0, -90, 0), time).SetEase(Ease.InQuad);
            transform.DOScale(new Vector3(-1, 1, 1), 0).SetDelay(time);
            transform.DORotate(new Vector3(0, 90, 0), 0).SetDelay(time).SetEase(Ease.OutQuad);
            transform.DORotate(new Vector3(0, 0, 0), time).SetDelay(time);
        }
        transform.DOMove(position, .15f);
    }
}
