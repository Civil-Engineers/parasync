using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{   
    [SerializeField] private float turnTimeInSeconds = 2f;
    [SerializeField] private Image timerImage;
    [SerializeField] private InputReader inputReader;

    [SerializeField] private int maxIterations = 4;
    private int _currIterations;

    private float _maxTime;
    private float _timeRemaining;

    private bool _isRunning = true;

    // Start is called before the first frame update
    void Start()
    {
       _maxTime = turnTimeInSeconds;
       _timeRemaining = turnTimeInSeconds;
       _currIterations = maxIterations;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isRunning)
        {
            _isRunning = true;
            ResetTime();

            if (_currIterations == 0)
                ResetIterations();
        }
        else
        {
            if (_timeRemaining >= 0)
            {
                _timeRemaining -= Time.deltaTime;
                timerImage.fillAmount = _timeRemaining / _maxTime;
            }
            else {
                _timeRemaining = 0;
                _currIterations--;
                GameObject[] attackMarkers = GameObject.FindGameObjectsWithTag("Respawn");
                foreach(GameObject marker in attackMarkers) {
                    GameObject.Destroy(marker);
                }
                _isRunning = false;
            }
        }
    }

    public void ResetTime()
    {
        _timeRemaining = turnTimeInSeconds;
    }

    public void ResetIterations()
    {
        _currIterations = maxIterations;
    }

    public bool IsRunning { get { return _isRunning; } }
    public int MaxIterations { get { return maxIterations; } }
    public int CurrIterations { get { return _currIterations; } }
    public float StartingTime { get { return turnTimeInSeconds; } }
    public float TimeRemaining { get { return _timeRemaining; } }
}
