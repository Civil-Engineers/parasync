using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{   
    [SerializeField] private float turnTimeInSeconds = 5.00f;
    [SerializeField] private Image timerImage;
    [SerializeField] private InputReader inputReader;

    [HideInInspector] public bool isTimerRunning = true;
    [HideInInspector] public int maxIterations = 2;
    [HideInInspector] public int currIterations;

    private float _maxTime;
    [HideInInspector] public float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
       _maxTime = turnTimeInSeconds;
       timeRemaining = turnTimeInSeconds;
       currIterations = maxIterations;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            ResetTime();

            if (currIterations == 0)
                ResetIterations();
        }
        else
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                timerImage.fillAmount = timeRemaining / _maxTime;
            }
            else
            {
                timeRemaining = 0.00f;
                isTimerRunning = false;
                inputReader.ResetPlayerMove();
                --currIterations;
            }
        }
    }

    public void ResetTime()
    {
        timeRemaining = turnTimeInSeconds;
    }

    public void ResetIterations()
    {
        currIterations = maxIterations;
    }
}
