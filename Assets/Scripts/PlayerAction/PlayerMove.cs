using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Transform sprite;
    [SerializeField] private MovementAction[] actions;

    private Dictionary<string, MovementAction> _p1MoveActions;
    private Dictionary<string, MovementAction> _p2MoveActions;

    private bool _isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        _p1MoveActions = new Dictionary<string, MovementAction>();
        _p2MoveActions = new Dictionary<string, MovementAction>();

        foreach (MovementAction action in actions)
        {
            string p1Actions = string.Join("", action.p1Actions);
            string p2Actions = string.Join("", action.p2Actions);
            _p1MoveActions.Add(p1Actions, action);
            _p2MoveActions.Add(p2Actions, action);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            string ctrlName = context.control.displayName;

            if (_p1MoveActions.ContainsKey(ctrlName))
                StartCoroutine(MoveCoroutine(_p1MoveActions[ctrlName]));
            if (_p2MoveActions.ContainsKey(ctrlName))
                StartCoroutine(MoveCoroutine(_p2MoveActions[ctrlName]));
        }
    }

    IEnumerator MoveCoroutine(MovementAction action)
    {
        Vector3 newPos = action.TriggerAction(playerObject);
        Tween(newPos, action.faceRight);
        yield return new WaitForSeconds(action.moveDelayInMs / 1000f);
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
