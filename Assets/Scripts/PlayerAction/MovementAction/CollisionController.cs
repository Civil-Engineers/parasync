using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private GameObject Top = null;
    private GameObject TopRight = null;
    private GameObject Right = null;
    private GameObject BottomRight = null;
    private GameObject Bottom = null;
    private GameObject BottomLeft = null;
    private GameObject Left = null;
    private GameObject TopLeft = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetTop() {
        return Top;
    }
    public GameObject GetTopRight() {
        return TopRight;
    }
    public GameObject GetRight() {
        return Right;
    }
    public GameObject GetBottomRight() {
        return BottomRight;
    }
    public GameObject GetBottom() {
        return Bottom;
    }
    public GameObject GetBottomLeft() {
        return BottomLeft;
    }
    public GameObject GetLeft() {
        return Left;
    }
    public GameObject GetTopLeft() {
        return TopLeft;
    }

    public void UpdateCollider(string side, GameObject item)
    {
        Debug.Log(side);
        switch (side)
        {
            case "t":
                Top = item;
                break;
            case "tr":
                TopRight = item;
                break;
            case "r":
                Right = item;
                break;
            case "br":
                BottomRight = item;
                break;
            case "b":
                Bottom = item;
                break;
            case "bl":
                BottomLeft = item;
                break;
            case "l":
                Left = item;
                break;
            case "tl":
                TopLeft = item;
                break;
        }
    }
}
