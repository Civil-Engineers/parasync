using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    [Header("Default Action Fields")]
    [SerializeField] private string actionName = "New Action";
    [Tooltip("Player 1 - List of consecutive inputs to press to perform this action")]
    [SerializeField] private string[] p1Actions;
    [Tooltip("Player 2 - List of consecutive inputs to press to perform this action")]
    [SerializeField] private string[] p2Actions;

    public string ActionName { get { return actionName; } }
    public string[] P1Actions { get {  return p1Actions; } }
    public string[] P2Actions { get {  return p2Actions; } }
}
