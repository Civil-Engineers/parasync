using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputReader : MonoBehaviour
{
    [SerializeField] private AttackBox[] player1;
    [SerializeField] private AttackBox[] player2;

    public enum Player { player1, player2 };


    int actionIndex = 0;



    [SerializeField] private AttackBox a;

    public void SetAmount(int amount)
    {
        for (int i = 0; i < player1.Length; i++)
        {
            if (i < amount)
            {
                player1[i].gameObject.SetActive(true);
                player2[i].gameObject.SetActive(true);
            }
            else
            {
                player1[i].gameObject.SetActive(false);
                player2[i].gameObject.SetActive(false);
            }
        }
    }


    public void StartQueue(int index, float startingTime)
    {
        player1[index].StartQueue(startingTime);
        player2[index].StartQueue(startingTime);
        actionIndex = index;
    }

    public void Register(Player player, AttackBox.Direction direction)
    {
        if (player == Player.player1)
        {
            player1[actionIndex].Register(direction);
        }
        if (player == Player.player2)
        {
            player2[actionIndex].Register(direction);
        }
    }

    public void Move(int index, AttackBox.Direction direction)
    {
        player1[index].Move(direction);
        player2[index].Move(direction);
    }

    public void Fade(int index)
    {
        player1[index].Fade();
        player2[index].Fade();
    }




    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetPlayerMove()
    {
    }

    public void AddPlayerOneInput(string playerOneInput)
    {
    }

    public void AddPlayerTwoInput(string playerTwoInput)
    {
    }

}
