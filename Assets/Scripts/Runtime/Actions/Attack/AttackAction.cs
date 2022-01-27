using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parasync.Runtime.Actions.Attack
{
    [CreateAssetMenu(menuName = "Actions/AttackAction")]
    public class AttackAction : BaseAction
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
}
