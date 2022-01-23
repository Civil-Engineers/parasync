using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Movement : MonoBehaviour
{

    [SerializeField] private float speed = 5;
    public InputReader inputReader;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {

    }

    public void OnUp()
    {
        transform.position += new Vector3(0, 0, speed);
        inputReader.AddPlayerOneInput("/\\");
        inputReader.AddPlayerTwoInput("/\\");
    }
    public void OnDown()
    {
        transform.position += new Vector3(0, 0, -speed);
        inputReader.AddPlayerOneInput("\\/");
        inputReader.AddPlayerTwoInput("\\/");
    }
    public void OnLeft()
    {
        transform.position += new Vector3(-speed, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        inputReader.AddPlayerOneInput("<-");
        inputReader.AddPlayerTwoInput("<-");
    }
    public void OnRight()
    {
        transform.position += new Vector3(speed, 0, 0);
        transform.rotation = Quaternion.Euler(0, -180, 0);
        inputReader.AddPlayerOneInput("->");
        inputReader.AddPlayerTwoInput("->");
    }
}
