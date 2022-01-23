using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private MovementAction[] actions;
    [SerializeField] private int numberOfMaxActions;
    [SerializeField] private InputReader inputReader;

    private Dictionary<string, MovementAction> _p1MoveActions;
    private Dictionary<string, MovementAction> _p2MoveActions;

    private string _p1Key;
    private string _p2Key;

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

    // Update is called once per frame
    void Update()
    {
        if (!timer.isTimerRunning)
        {
            Debug.Log(_p1Key + " " + _p2Key);
            _p1Key = null;
            _p2Key = null;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        string ctrlName = context.control.displayName;

        if (context.action.triggered)
        {
            if (_p1MoveActions.ContainsKey(ctrlName))
                _p1Key = ctrlName;
            if (_p2MoveActions.ContainsKey(ctrlName))
                _p2Key = ctrlName;
        }
    }
}
