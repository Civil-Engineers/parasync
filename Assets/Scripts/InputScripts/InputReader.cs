using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputReader : MonoBehaviour
{
    [SerializeField] private AttackBox[] player1;
    [SerializeField] private AttackBox[] player2;

    public enum Player {player1, player2};


    int actionIndex = 0;



    [SerializeField] private AttackBox a;


    public void StartQueue(int index) {
        player1[index].StartQueue();
        player2[index].StartQueue();
        actionIndex = 0;
    }

    public void Register(Player player, AttackBox.Direction direction) {
        if (player == Player.player1) {
            player1[actionIndex].Register(direction);
        }
        if (player == Player.player2) {
            player2[actionIndex].Register(direction);
        }
    }

    public void Move(int index) {
        player1[index].Move();
        player2[index].Move();
    }


    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetPlayerMove() {
    }

    public void AddPlayerOneInput(string playerOneInput) {
    }

    public void AddPlayerTwoInput(string playerTwoInput) {
    }

}
