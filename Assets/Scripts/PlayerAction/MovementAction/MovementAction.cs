using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/MovementAction")]
public class MovementAction : Action
{
    [Header("Movement Action Fields")]
    [Tooltip("Number of units this action causes an entity to move")]
    [SerializeField] private int moveMagnitude = 1;
    [Tooltip("Direction to move the entity in")]
    [SerializeField] private Vector2 moveDirection;
    public bool faceRight;
    public float moveDelayInMs = 250f;

    public Vector3 TriggerAction(GameObject obj)
    {
        Vector3 currPos = obj.transform.position;
        float newX = moveMagnitude * moveDirection.x;
        float newZ = moveMagnitude * moveDirection.y;

        currPos += new Vector3(newX, 0, newZ);
        return currPos;
    }
}
