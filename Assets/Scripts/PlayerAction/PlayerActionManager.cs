using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

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

    // Start is called before the first frame update
    void Start()
    {
        _p1MoveActions = new Dictionary<string, MovementAction>();
        _p2MoveActions = new Dictionary<string, MovementAction>();

        _p1KeyPresses = new List<string>();
        _p2KeyPresses = new List<string>();

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
            _p1KeyPresses.Add(_p1Key);
            _p2KeyPresses.Add(_p2Key);

            _p1Key = null;
            _p2Key = null;
        }

        if (timer.currIterations == 0)
        {
            Debug.Log("Completed entire sequence");
            string p1Keys = string.Join(",", _p1KeyPresses);
            string p2Keys = string.Join(",", _p2KeyPresses);
            Debug.Log("P1: " + p1Keys);
            Debug.Log("P2: " + p2Keys);

            for (int i = 0; i < _p1KeyPresses.Count; i++)
            {
                string key1 = _p1KeyPresses[i], key2 = _p2KeyPresses[i];

                if (key1 != null && key2 != null)
                {
                    if (_p1MoveActions.ContainsKey(key1))
                        Debug.Log("Execute " + key1);
                    if (_p2MoveActions.ContainsKey(key2))
                        Debug.Log("Execute " + key2);
                }
                else Debug.Log("One player forgot to enter an action! Not executing...");
            }

            _p1KeyPresses.Clear();
            _p2KeyPresses.Clear();
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
