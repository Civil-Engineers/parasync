using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{   
    private const float maxSeconds = 5.00f;
    [SerializeField] private float secondsRemaining = 5.00f;
    public bool IsTimerRunning = true;

    public Image timer;
    public InputReader inputReader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsTimerRunning) {
            
            if(secondsRemaining >= 0) {
                secondsRemaining -= Time.deltaTime;
                timer.fillAmount = secondsRemaining / maxSeconds;
            }
            else {
                secondsRemaining = 0.00f;
                IsTimerRunning = false;
                inputReader.ResetPlayerMove();
            }
        } 
    }

    void DisplaySeconds(float secondsToDisplay) {
        float seconds = Mathf.FloorToInt(secondsToDisplay % 60);
    }

    public void ResetTime() {
        secondsRemaining = 5.00f;
    }
}
