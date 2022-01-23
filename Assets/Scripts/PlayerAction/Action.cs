using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    [Header("Default Action Fields")]
    public string actionName = "New Action";
    [Tooltip("Player 1 - List of consecutive inputs to press to perform this action")]
    public string[] p1Actions;
    [Tooltip("Player 2 - List of consecutive inputs to press to perform this action")]
    public string[] p2Actions;
}
