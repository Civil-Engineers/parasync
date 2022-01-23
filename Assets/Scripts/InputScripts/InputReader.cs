using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputReader : MonoBehaviour
{
    public bool playerMove = true;
    // Start is called before the first frame update
    public List<string> playerOneSequence = new List<string>();
    public List<string> playerTwoSequence = new List<string>();
    public TMP_Text displayInput;
    string pOneString = "";
    string pTwoString = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMove) {
            //Get the list of inputs from Riki's script
            //For now I test using my own input
            
            if(playerOneSequence.Count > playerTwoSequence.Count) {
                for(int i = 0; i < playerTwoSequence.Count; i++) {  
                    pOneString = pOneString + playerOneSequence[i] + "  ";
                    pTwoString = pTwoString + playerTwoSequence[i] + "  ";
                    displayInput.text = pOneString + "\n" + pTwoString;
                }
            } else {
                for(int i = 0; i < playerOneSequence.Count; i++) {  
                    pOneString = pOneString + playerOneSequence[i] + "  ";
                    pTwoString = pTwoString + playerTwoSequence[i] + "  ";
                    displayInput.text = pOneString + "\n" + pTwoString;
                }
            } 
            playerMove = false;
        }
    }

    public void ResetPlayerMove() {
        playerMove = true;
    }

    public void AddPlayerOneInput(string playerOneInput) {
        playerOneSequence.Add(playerOneInput);
    }

    public void AddPlayerTwoInput(string playerTwoInput) {
        playerTwoSequence.Add(playerTwoInput);
    }

}
