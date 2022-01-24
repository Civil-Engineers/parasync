using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/AttackAction")]
public class AttackAction : Action
{
    public void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public void TriggerAction()
    {
        Debug.Log("Triggered " + ActionName + " attack action");
    }
}
