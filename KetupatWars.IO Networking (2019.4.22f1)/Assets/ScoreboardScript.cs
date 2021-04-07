using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardScript : Photon.MonoBehaviour
{
    [SerializeField]PlayerScript PlayerData;

    public Text PlayerName;
    public Text PlayerScore;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerData.GetComponent<PlayerScript>();
    }

    public void SetPlayer(PlayerScript _player)
    {
        PlayerData = _player;

    }

    // Update is called once per frame
    void Update()
    {
        GetScore();
        GetName();
    }

    void GetScore()
    {
        PlayerScore.text = PlayerData.berasDimiliki.ToString();
    }

    void GetName()
    {
        
    }
}
