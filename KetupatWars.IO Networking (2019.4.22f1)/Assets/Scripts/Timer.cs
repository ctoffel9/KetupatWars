using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class Timer : Photon.MonoBehaviour
{
    public float timeRemaining = 10;
    public float localTimer;

    public bool timerIsOn = false;

    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timerIsOn = true;
    }

    // Update is called once per frame
    [PunRPC]
    void Update()
    {
        RunTimer();
    }

    public void RunTimer()
    {
        localTimer = timeRemaining;

        if(timerIsOn)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsOn = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
