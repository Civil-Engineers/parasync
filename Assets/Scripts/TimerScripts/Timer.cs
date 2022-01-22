using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{   
    [SerializeField] private float secondsRemaining = 5.00f;
    public bool IsTimerRunning = true;

    public TextMeshProUGUI TextField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsTimerRunning) {
            
            if(secondsRemaining > 0) {
                secondsRemaining -= Time.deltaTime;
                double displayTime = System.Math.Round(secondsRemaining, 2);
                TextField.text = displayTime.ToString() + " seconds";
            }
            else {
                secondsRemaining = 0.00f;
                IsTimerRunning = false;
                TextField.text = secondsRemaining + " seconds";
            }
        } 
    }

    void DisplaySeconds(float secondsToDisplay) {
        float seconds = Mathf.FloorToInt(secondsToDisplay % 60);
    }
}
