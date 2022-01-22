using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] private float speed = 5;

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
    }
    public void OnDown()
    {
        transform.position += new Vector3(0, 0, -speed);
    }
    public void OnLeft()
    {
        transform.position += new Vector3(-speed, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void OnRight()
    {
        transform.position += new Vector3(speed, 0, 0);
        transform.rotation = Quaternion.Euler(0, -180, 0);
    }
}
