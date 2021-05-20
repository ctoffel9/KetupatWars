using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class ScoreboardScript : Photon.PunBehaviour , IPunObservable
{
    [SerializeField] PlayerScript PlayerData;

    public Text PlayerName;
    public Text PlayerScore;
    // Start is called before the first frame update
    void Awake()
    {
        //PlayerData.GetComponent<PlayerScript>();
    }

    public void SetPlayer(PlayerScript _player)
    {
        PlayerData = _player;

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScore != null)
            PlayerScore.text = PlayerData.berasDimiliki.ToString();
    }

    void GetScore()
    {
        PlayerScore.text = PlayerData.berasDimiliki.ToString();
    }

    void GetName()
    {

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(PlayerData.berasDimiliki);
        }
        else
        {
            PlayerData.berasDimiliki = (float)stream.ReceiveNext();
        }
    }


}
