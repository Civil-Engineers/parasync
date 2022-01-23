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

    private int currentActionIndex = 0;

    private bool isTimerRunnningBefore = false;
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

        inputReader.StartQueue(0, timer.getStartingTime());
    }

    // Update is called once per frame
    void Update()
    {

        if (!timer.isTimerRunning)
        {
            _p1KeyPresses.Add(_p1Key);
            _p2KeyPresses.Add(_p2Key);

            inputReader.StartQueue(_p2KeyPresses.Count, timer.getStartingTime());


            _p1Key = null;
            _p2Key = null;
        }

        if (timer.currIterations == 0)
        {
            Debug.Log("_p1KeyPresses: "+_p1KeyPresses.Count);

            for (int i = 0; i < _p1KeyPresses.Count; i++)
            {
                string key1 = _p1KeyPresses[i], key2 = _p2KeyPresses[i];

                if (key1 != null && key2 != null)
                {
                    
                }
                else Debug.Log("One player forgot to enter an action! Not executing...");
            }

            _p1KeyPresses.Clear();
            _p2KeyPresses.Clear();
            

            inputReader.StartQueue(0, timer.getStartingTime());
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        string ctrlName = context.control.displayName;

        if (context.action.triggered)
        {
            if (_p1MoveActions.ContainsKey(ctrlName)) {
                _p1Key = ctrlName;
                AttackBox.Direction dir = AttackBox.Direction.Up;
                switch(_p1Key) {
                    case "W":
                        dir = AttackBox.Direction.Up;
                        break;
                    case "A":
                        dir = AttackBox.Direction.Left;
                        break;
                    case "S":
                        dir = AttackBox.Direction.Down;
                        break;
                    case "D":
                        dir = AttackBox.Direction.Right;
                        break;
                }
                inputReader.Register(InputReader.Player.player1, dir);
            }
                
            if (_p2MoveActions.ContainsKey(ctrlName)) {
                _p2Key = ctrlName;
                AttackBox.Direction dir = AttackBox.Direction.Up;
                switch(_p2Key) {
                    case "Up":
                        dir = AttackBox.Direction.Up;
                        break;
                    case "Left":
                        dir = AttackBox.Direction.Left;
                        break;
                    case "Down":
                        dir = AttackBox.Direction.Down;
                        break;
                    case "Right":
                        dir = AttackBox.Direction.Right;
                        break;
                }
                inputReader.Register(InputReader.Player.player2, dir);
            }

        }
    }

    private MovementAction returnCombo(string key1, string key2) {
        if(key1 == "W" && key2 == "Left" || key1 == "A" && key2 == "Up") {
            return _p1MoveActions["Move Top Left"];
        }
        else if(key1 == "W" && key2 == "Right" || key1 == "D" && key2 == "Up") {
            return _p1MoveActions["Move Top Right"];
        }
        else if(key1 == "S" && key2 == "Left" || key1 == "A" && key2 == "Bottom") {
            return _p1MoveActions["Move Bottom Left"];
        }
        else if(key1 == "S" && key2 == "Right" || key1 == "D" && key2 == "Bottom") {
            return _p1MoveActions["Move Bottom Right"];
        } else {
            return null;
        }
    }

}
