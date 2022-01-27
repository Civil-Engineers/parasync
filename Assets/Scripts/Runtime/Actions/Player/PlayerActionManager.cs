using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using DG.Tweening;
using Parasync.Runtime.Actions.Movement;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Transform playerSprite;

    [SerializeField] private MovementAction[] actions;

    [Header("Event Listeners")]
    [SerializeField] private UnityEvent<int, Vector2> onMove;
    [SerializeField] private UnityEvent<int, Vector2> onActionsCombine;
    [SerializeField] private UnityEvent<int> onActionsFailedToCombine;
    [SerializeField] private UnityEvent onPlayerTurnEnd;

    private Dictionary<string, MovementAction> _p1MoveActions, _p2MoveActions;
    private List<string> _p1KeyPresses, _p2KeyPresses;
    private string _p1Key = null, _p2Key = null;
    private bool _isFacingRight = true;

    private List<Vector2> _movementList;
    private List<bool> _faceList;

    private bool _actionsCombined = false;
    private bool _actionsFailedToBeCombined = false;

    private void Start()
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
                onMove?.Invoke(1, _p1MoveActions[ctrlName].MoveDirection);
            }

            if (_p2MoveActions.ContainsKey(ctrlName))
            {
                _p2Key = ctrlName;
                onMove?.Invoke(2, _p2MoveActions[ctrlName].MoveDirection);
            }
        }
    }

    public void OnIterationEnd()
    {
        _p1KeyPresses.Add(_p1Key);
        _p2KeyPresses.Add(_p2Key);

        _p1Key = null;
        _p2Key = null;
    }

    public void OnTurnEnd()
    {
        _movementList = new List<Vector2>();
        _faceList = new List<bool>();

        for (int i = 0; i < _p1KeyPresses.Count; i++)
        {
            string key1 = _p1KeyPresses[i], key2 = _p2KeyPresses[i];

            if (key1 != null && key2 != null)
            {
                MovementAction action1 = _p1MoveActions[key1];
                MovementAction action2 = _p2MoveActions[key2];

                Vector2 combinedVector = CombineMovement(action1, action2);
                bool faceRight = CombineFacing(action1, action2);

                _movementList.Add(combinedVector);
                _faceList.Add(faceRight);
            }
            else
            {
                _movementList.Add(Vector2.zero);
                _faceList.Add(false);
            }
        }

        StartCoroutine(MoveCoroutine(0, _movementList, _faceList));

        _p1KeyPresses.Clear();
        _p2KeyPresses.Clear();
    }

    public void OnActionsCombined() => _actionsCombined = true;

    public void OnActionsFailedToBeCombined() => _actionsFailedToBeCombined = true;

    IEnumerator MoveCoroutine(int index, List<Vector2> movements, List<bool> faces)
    {
        while(index < movements.Count)
        {
            Vector2 direction = movements[index];
            bool faceRight = faces[index];

            if (direction != Vector2.zero)
            {
                onActionsCombine?.Invoke(index, direction);

                yield return new WaitUntil(() => _actionsCombined);
                yield return new WaitForSeconds(0.5f);

                MovePlayer(new Vector3(direction.x, 0, direction.y), faceRight);
                _actionsCombined = false;
            }
            else
            {
                onActionsFailedToCombine?.Invoke(index);

                yield return new WaitUntil(() => _actionsFailedToBeCombined);
                yield return new WaitForSeconds(0.5f);
            
                _actionsFailedToBeCombined = false;
            }

            yield return new WaitForSeconds(0.5f);

            index++;
        }
        onPlayerTurnEnd?.Invoke();
    }

    private void MovePlayer(Vector3 moveVector, bool faceRight)
    {
        Vector3 currPos = playerObject.transform.position;
        currPos += moveVector;
        TweenMovement(currPos, faceRight);
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

    private void TweenMovement(Vector3 target, bool faceRight)
    {
        float time = 0.2f;

        if (!_isFacingRight && faceRight)
        {
            _isFacingRight = true;
            playerSprite.DORotate(new Vector3(0, -90, 0), time).SetEase(Ease.InQuad);
            playerSprite.DOScale(new Vector3(1, 1, 1), 0).SetDelay(time);
            playerSprite.DORotate(new Vector3(0, 90, 0), 0).SetDelay(time).SetEase(Ease.OutQuad);
            playerSprite.DORotate(new Vector3(0, 0, 0), time).SetDelay(time);
        }
        else if (_isFacingRight && !faceRight)
        {
            _isFacingRight = false;
            playerSprite.DORotate(new Vector3(0, -90, 0), time).SetEase(Ease.InQuad);
            playerSprite.DOScale(new Vector3(-1, 1, 1), 0).SetDelay(time);
            playerSprite.DORotate(new Vector3(0, 90, 0), 0).SetDelay(time).SetEase(Ease.OutQuad);
            playerSprite.DORotate(new Vector3(0, 0, 0), time).SetDelay(time);
        }
        playerObject.transform.DOMove(target, 0.15f);
    }
}
