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
    private bool _isFacingRight = true;

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
            List<MovementAction> movementList = new List<MovementAction>();
            for (int i = 0; i < _p1KeyPresses.Count; i++)
            {
                string key1 = _p1KeyPresses[i], key2 = _p2KeyPresses[i];
                if (key1 != null && key2 != null)
                {
                    movementList.Add(returnCombo(key1, key2));
                }
                else
                {
                    movementList.Add(null);
                };
            }


            for (int i = 0; i < movementList.Count; i++) {
                MovementAction action = movementList[i];
                StartCoroutine(MoveCoroutine(i, action, i));
            }

            _p1KeyPresses.Clear();
            _p2KeyPresses.Clear();


            inputReader.StartQueue(0, timer.getStartingTime());
        }
    }

    IEnumerator MoveCoroutine(float delay, MovementAction action, int index) {

        yield return new WaitForSeconds(delay);

        if (action != null) {
            Move(action);
            inputReader.Move(index, GetDirection(action));
        }

    }

    AttackBox.Direction GetDirection(MovementAction action)
    {
        AttackBox.Direction dir = AttackBox.Direction.Up;
        switch (action.actionName)
        {
            case "Move Top Left":
                dir = AttackBox.Direction.UpLeft;
                break;
            case "Move Top Right":
                dir = AttackBox.Direction.UpRight;
                break;
            case "Move Bottom Left":
                dir = AttackBox.Direction.DownLeft;
                break;
            case "Move Bottom Right":
                dir = AttackBox.Direction.DownRight;
                break;
            case "Move Forward":
                dir = AttackBox.Direction.Up;
                break;
            case "Move Left":
                dir = AttackBox.Direction.Left;
                break;
            case "Move Backward":
                dir = AttackBox.Direction.Down;
                break;
            case "Move Right":
                dir = AttackBox.Direction.Right;
                break;
        }
        return dir;
    }


    void Move(MovementAction action)
    {
        Vector3 newPos = action.TriggerAction(playerObject);
        Tween(newPos, action.faceRight);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        string ctrlName = context.control.displayName;

        if (context.action.triggered)
        {
            if (_p1MoveActions.ContainsKey(ctrlName))
            {
                _p1Key = ctrlName;
                AttackBox.Direction dir = AttackBox.Direction.Up;
                switch (_p1Key)
                {
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

            if (_p2MoveActions.ContainsKey(ctrlName))
            {
                _p2Key = ctrlName;
                AttackBox.Direction dir = AttackBox.Direction.Up;
                switch (_p2Key)
                {
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

    private MovementAction returnCombo(string key1, string key2)
    {
        if ((key1 == "W" && key2 == "Left") || (key1 == "A" && key2 == "Up"))
        {
            return _p1MoveActions["Move Top Left"];
        }
        else if ((key1 == "W" && key2 == "Right") || (key1 == "D" && key2 == "Up"))
        {
            return _p1MoveActions["Move Top Right"];
        }
        else if ((key1 == "S" && key2 == "Left") || (key1 == "A" && key2 == "Bottom"))
        {
            return _p1MoveActions["Move Bottom Left"];
        }
        else if ((key1 == "S" && key2 == "Right") || (key1 == "D" && key2 == "Bottom"))
        {
            return _p1MoveActions["Move Bottom Right"];
        }
        else if ((key1 == "W" && key2 == "Up"))
        {
            return _p1MoveActions["W"];
        }
        else if ((key1 == "S" && key2 == "Down"))
        {
            return _p1MoveActions["S"];
        }
        else if ((key1 == "D" && key2 == "Right"))
        {
            return _p1MoveActions["D"];
        }
        else if ((key1 == "A" && key2 == "Left"))
        {
            return _p1MoveActions["A"];
        }
        else
        {
            Debug.Log("Opposite returned :(");
            return null;
        }
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
