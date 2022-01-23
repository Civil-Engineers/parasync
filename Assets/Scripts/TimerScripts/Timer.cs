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
    [SerializeField] private int maxIterations = 4;
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
            else {
                timeRemaining = 0;

                inputReader.ResetPlayerMove();
                currIterations--;
                Debug.Log("currIterations: " + currIterations);
                GameObject[] attackMarkers = GameObject.FindGameObjectsWithTag("Respawn");
                foreach(GameObject marker in attackMarkers) {
                    GameObject.Destroy(marker);
                }
                isTimerRunning = false;
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

    public float getStartingTime() {
        return turnTimeInSeconds;
    }
}
