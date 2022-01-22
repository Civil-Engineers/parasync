using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveDelayInMs = 250f;
    [SerializeField] private float speed = 5;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log(context.control.displayName);
            Vector2 direction = context.ReadValue<Vector2>();
            float x = direction.normalized.x;
            float y = direction.normalized.y;
            float newX = 6.4f / 2;
            float newY = 3.2f / 2;
            if (x == 0 && y == -1) { newX *= -1; newY *= -1; }
            else if (x == -1 && y == 0) newX *= -1;
            else if (x == 1 && y == 0) newY *= -1;
            StartCoroutine(MoveCoroutine(new Vector3(newX, newY, 0)));
        }
    }

    IEnumerator MoveCoroutine(Vector3 movement)
    {
        transform.position += movement;

        yield return new WaitForSeconds(moveDelayInMs / 1000f);
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
