using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Parasync.Runtime.Components.Timers;
using Parasync.Runtime.Actions.Movement;

namespace Parasync.Runtime.Enemies
{
    public class RangedScript : MonoBehaviour
    {
        public Timer timerObject;
        public GameObject playerObject;
        [SerializeField] private Transform sprite;
        [SerializeField] private GameObject enemyObject;
        [SerializeField] private MovementAction[] actions;

        private Dictionary<string, MovementAction> _enemyActions;

        private bool _isFacingRight = true;
        void Start()
        {
            _enemyActions = new Dictionary<string, MovementAction>();

            foreach (MovementAction action in actions)
                _enemyActions.Add(action.ActionName, action);
        }

        // Update is called once per frame
        void Update()
        {
            //if(!timerObject.IsRunning) {
            //    float currX = enemyObject.transform.position.x;
            //    float currZ = enemyObject.transform.position.z;
            //    Vector3 upDir = new Vector3(currX, 0, currZ + 1); 
            //    Vector3 downDir = new Vector3(currX, 0, currZ - 1); 
            //    Vector3 leftDir = new Vector3(currX - 1, 0, currZ); 
            //    Vector3 rightDir = new Vector3(currX + 1, 0, currZ); 

            //    float magnitudeUp = (playerObject.transform.position - upDir).magnitude;
            //    float magnitudeDown = (playerObject.transform.position - downDir).magnitude;
            //    float magnitudeLeft = (playerObject.transform.position - leftDir).magnitude;
            //    float magnitudeRight = (playerObject.transform.position - rightDir).magnitude;

            //    float[] magArray = new float[] { magnitudeUp, magnitudeDown, magnitudeLeft, magnitudeRight };
            //    float maxMag = Mathf.Max(magArray);
                
            //    Debug.Log(maxMag);
            //    if(maxMag == magnitudeUp) { 
            //        Vector3 newPos = _enemyActions["Move Forward"].TriggerAction(enemyObject);
            //        Tween(newPos, _enemyActions["Move Forward"].FaceRight);
            //    }
            //    else if(maxMag == magnitudeDown) {  
            //        Vector3 newPos = _enemyActions["Move Backward"].TriggerAction(enemyObject);
            //        Tween(newPos, _enemyActions["Move Backward"].FaceRight);
            //    }
            //    else if(maxMag == magnitudeRight) {  
            //        Vector3 newPos = _enemyActions["Move Right"].TriggerAction(enemyObject);
            //        Tween(newPos, _enemyActions["Move Right"].FaceRight);
            //    }
            //    else if(maxMag == magnitudeLeft) {
            //        Vector3 newPos = _enemyActions["Move Left"].TriggerAction(enemyObject);
            //        Tween(newPos, _enemyActions["Move Left"].FaceRight);
            //    } else {
            //        Debug.Log("We have a problem dude.");
            //    }
            //}   
        }

        private void Tween(Vector3 target, bool faceRight)
        {
            float time = 0.2f;

            if (!_isFacingRight && faceRight)
            {
                _isFacingRight = true;
                sprite.DORotate(new Vector3(0, -90, 0), time).SetEase(Ease.InQuad);
                sprite.DOScale(new Vector3(1, 1, 1), 0).SetDelay(time);
                sprite.DORotate(new Vector3(0, 90, 0), 0).SetDelay(time).SetEase(Ease.OutQuad);
                sprite.DORotate(new Vector3(0, 0, 0), time).SetDelay(time);
            }
            else if (_isFacingRight && !faceRight)
            {
                _isFacingRight = false;
                sprite.DORotate(new Vector3(0, -90, 0), time).SetEase(Ease.InQuad);
                sprite.DOScale(new Vector3(-1, 1, 1), 0).SetDelay(time);
                sprite.DORotate(new Vector3(0, 90, 0), 0).SetDelay(time).SetEase(Ease.OutQuad);
                sprite.DORotate(new Vector3(0, 0, 0), time).SetDelay(time);
            }
            enemyObject.transform.DOMove(target, 0.15f);
        }
    }
}
