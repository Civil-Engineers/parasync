using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Timer timerObject;
    public int speed = 1;
    public PlayerMove playerObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!timerObject.IsTimerRunning) {
            Vector3 upDir = new Vector3(transform.position.x, 0, transform.position.z + 1); 
            Vector3 downDir = new Vector3(transform.position.x, 0, transform.position.z - 1); 
            Vector3 leftDir = new Vector3(transform.position.x - 1, 0, transform.position.z); 
            Vector3 rightDir = new Vector3(transform.position.x + 1, 0, transform.position.z); 

            float magnitudeUp = (playerObj.transform.position - upDir).magnitude;
            float magnitudeDown = (playerObj.transform.position - downDir).magnitude;
            float magnitudeLeft = (playerObj.transform.position - leftDir).magnitude;
            float magnitudeRight = (playerObj.transform.position - rightDir).magnitude;

            float[] magArray = new float[] { magnitudeUp, magnitudeDown, magnitudeLeft, magnitudeRight };
            float minMag = Mathf.Min(magArray);
            
            Debug.Log(minMag.ToString());
            if(minMag == magnitudeUp) { MoveUp(); }
            else if(minMag == magnitudeDown) { MoveDown(); }
            else if(minMag == magnitudeRight) { MoveRight(); }
            else { MoveLeft(); }
            timerObject.IsTimerRunning = true;
            timerObject.ResetTime();
        }   
    }

    public void MoveUp()
    {
        transform.position += new Vector3(0, 0, speed);
    }
    public void MoveDown()
    {
        transform.position += new Vector3(0, 0, -speed);
    }
    public void MoveLeft()
    {
        transform.position += new Vector3(-speed, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void MoveRight()
    {
        transform.position += new Vector3(speed, 0, 0);
        transform.rotation = Quaternion.Euler(0, -180, 0);
    }
}
