using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class Timer : Photon.MonoBehaviour , IPunObservable
{
    public float timeRemaining = 10;
    public float localTimer;

    public bool timerIsOn = false;

    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            photonView.RPC("TimerStarts", PhotonTargets.AllViaServer);
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (PhotonNetwork.isMasterClient)
        {
            photonView.RPC("timerOn", PhotonTargets.AllViaServer);
        }
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

    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.timeText);
            stream.SendNext(this.timerIsOn);
            stream.SendNext(this.timeRemaining);
            stream.SendNext(this.localTimer);
            
        }
        else
        {
            timeText = (Text)stream.ReceiveNext();
            timerIsOn = (bool)stream.ReceiveNext();
            timeRemaining = (float)stream.ReceiveNext();
            localTimer = (float)stream.ReceiveNext();

        }

    }
    [PunRPC]
    public void TimerStarts()
    {
        RunTimer();
    }

    [PunRPC]
    public void timerOn()
    {
        timerIsOn = true;
    }
}
