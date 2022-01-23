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

    private float _maxTime;
    private float _timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
       _maxTime = turnTimeInSeconds;
       _timeRemaining = turnTimeInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning) {
            if (_timeRemaining >= 0) {
                _timeRemaining -= Time.deltaTime;
                timerImage.fillAmount = _timeRemaining / _maxTime;
            }
            else {
                _timeRemaining = 0.00f;
                isTimerRunning = false;
                inputReader.ResetPlayerMove();
                GameObject[] attackMarkers = GameObject.FindGameObjectsWithTag("Respawn");
                foreach(GameObject marker in attackMarkers) {
                    GameObject.Destroy(marker);
                }
            }
        } 
    }

    public void ResetTime() {
        _timeRemaining = turnTimeInSeconds;
    }
}
