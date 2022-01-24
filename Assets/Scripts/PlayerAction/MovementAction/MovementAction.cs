using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/MovementAction")]
public class MovementAction : Action
{
    [Header("Movement Action Fields")]
    [Tooltip("Number of units this action causes an entity to move")]
    [Range(0, 10)]
    [SerializeField] private int moveMagnitude = 1;
    [Tooltip("Direction to move the entity in")]
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private bool faceRight;
    [SerializeField] private float moveDelayInMs = 250f;

    public Vector3 TriggerAction(GameObject obj)
    {
        Vector3 currPos = obj.transform.position;
        float newX = moveMagnitude * moveDirection.x;
        float newZ = moveMagnitude * moveDirection.y;

        currPos += new Vector3(newX, 0, newZ);
        return currPos;
    }

    public int MoveMagnitude { get { return moveMagnitude; } }
    public Vector2 MoveDirection { get { return moveDirection; } }
    public bool FaceRight { get {  return faceRight; } }
    public float MoveDelayInMs { get { return moveDelayInMs; } }
}
