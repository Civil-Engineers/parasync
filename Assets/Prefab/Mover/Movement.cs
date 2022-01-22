using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update() {
        
    }

    // public void OnMove(InputAction.CallbackContext context)
    // {
    //     Vector2 direction = context.ReadValue<Vector2>();
    //     if (direction.y > 1)
    //     {
    //         Up();
    //     }
    //     if (direction.y < 1)
    //     {
    //         Down();
    //     }
    //     if (direction.x > 1)
    //     {
    //         Right();
    //     }
    //     if (direction.x < 1)
    //     {
    //         Left();
    //     }
    // }


    public void OnUp()
    {
        Debug.Log("hi");
        transform.position += new Vector3(6.4f/2, 3.2f/2, 0);
    }
    public void OnDown()
    {
        transform.position += new Vector3(-6.4f/2, -3.2f/2, 0);
    }
    public void OnLeft()
    {
        transform.position += new Vector3(-6.4f/2, 3.2f/2, 0);
    }
    public void OnRight()
    {
        transform.position += new Vector3(6.4f/2, -3.2f/2, 0);
    }
}
