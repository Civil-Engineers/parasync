using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Parasync.Runtime.Components.Timers;
using Parasync.Runtime.Actions.Movement;
using Parasync.Runtime.UI;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Transform sprite;

    [SerializeField] private MovementAction[] actions;
    [SerializeField] private int numberOfMaxActions;

    [SerializeField] private Timer timer;
    [SerializeField] private InputReader inputReader;

    private Dictionary<string, MovementAction> _p1MoveActions, _p2MoveActions;
    private List<string> _p1KeyPresses, _p2KeyPresses;
    private string _p1Key = null, _p2Key = null;
    private bool _isFacingRight = true;

    private List<Vector2> _movementList;
    private List<bool> _faceList;

    // Start is called before the first frame update
    void Start()
    {
        _p1MoveActions = new Dictionary<string, MovementAction>();
        _p2MoveActions = new Dictionary<string, MovementAction>();

        _p1KeyPresses = new List<string>();
        _p2KeyPresses = new List<string>();

        foreach (MovementAction action in actions)
        {
            string p1Actions = string.Join("", action.P1Actions);
            string p2Actions = string.Join("", action.P2Actions);
            _p1MoveActions.Add(p1Actions, action);
            _p2MoveActions.Add(p2Actions, action);
        }

        //inputReader.SetAmount(timer.MaxIterations);
        //inputReader.StartQueue(0, timer.StartingTime);
    }

    // Update is called once per frame
    void Update()
    {
        //// When a single iteration is complete
        //if (!timer.IsRunning)
        //{
        //    Debug.Log(_p1Key + " " + _p2Key);
        //    _p1KeyPresses.Add(_p1Key);
        //    _p2KeyPresses.Add(_p2Key);

        //    if (_p1KeyPresses.Count < timer.MaxIterations)
        //        inputReader.StartQueue(_p1KeyPresses.Count, timer.StartingTime);

        //    _p1Key = null;
        //    _p2Key = null;
        //}

        //// When an entire sequence is completed, marked by number of iterations
        //// Triggered controls are processed and combined into a single vector
        //if (timer.CurrIterations == 0)
        //{
        //    _movementList = new List<Vector2>();
        //    _faceList = new List<bool>();

        //    for (int i = 0; i < _p1KeyPresses.Count; i++)
        //    {
        //        string key1 = _p1KeyPresses[i], key2 = _p2KeyPresses[i];

        //        if (key1 != null && key2 != null)
        //        {
        //            MovementAction action1 = _p1MoveActions[key1];
        //            MovementAction action2 = _p2MoveActions[key2];

        //            Vector2 combinedVector = CombineMovement(action1, action2);
        //            bool faceRight = CombineFacing(action1, action2);

        //            _movementList.Add(combinedVector);
        //            _faceList.Add(faceRight);
        //        }
        //        else
        //        {
        //            _movementList.Add(Vector2.zero);
        //            _faceList.Add(false);
        //        }
        //    }

        //    for (int i = 0; i < _movementList.Count; i++)
        //        StartCoroutine(MoveCoroutine(_movementList[i], _faceList[i], i));

        //    _p1KeyPresses.Clear();
        //    _p2KeyPresses.Clear();

        //    inputReader.StartQueue(0, timer.StartingTime);
        //}
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Keep updating triggered controls as long as the timer is running
        if (context.action.triggered)
        {
            string ctrlName = context.control.displayName;

            if (_p1MoveActions.ContainsKey(ctrlName))
            {
                _p1Key = ctrlName;
                inputReader.Register(InputReader.Player.player1, _p1MoveActions[ctrlName]);
            }
            if (_p2MoveActions.ContainsKey(ctrlName))
            {
                _p2Key = ctrlName;
                inputReader.Register(InputReader.Player.player2, _p2MoveActions[ctrlName]);
            }
        }
    }

    IEnumerator MoveCoroutine(Vector2 movement, bool faceRight, int index)
    {
        yield return new WaitForSeconds(index);

        if (movement != Vector2.zero)
        {
            Vector3 currPos = playerObject.transform.position;
            currPos += new Vector3(movement.x, 0, movement.y);
            Tween(currPos, faceRight);
            inputReader.Move(index, movement);
        }
        else inputReader.Fade(index);
    }

    private Vector3 CombineMovement(MovementAction p1Action, MovementAction p2Action)
    {
        float p1VectorX = p1Action.MoveMagnitude * p1Action.MoveDirection.x;
        float p1VectorY = p1Action.MoveMagnitude * p1Action.MoveDirection.y;

        float p2VectorX = p2Action.MoveMagnitude * p2Action.MoveDirection.x;
        float p2VectorY = p2Action.MoveMagnitude * p2Action.MoveDirection.y;

        return new Vector2(p1VectorX + p2VectorX, p1VectorY + p2VectorY);
    }

    private bool CombineFacing(MovementAction p1Action, MovementAction p2Action)
    {
        return p1Action.FaceRight || p2Action.FaceRight;
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
        playerObject.transform.DOMove(target, 0.15f);
    }
}
