using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScene : MonoBehaviour
{
    [SerializeField] private GameObject DeadInstance;
    public PlayerScript playerController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeathS()
    {
        Debug.Log("wanjay");
        DeadInstance.SetActive(true);
    }

    public void SetManager(PlayerScript _playerManager)
    {
        playerController = _playerManager;
    }

    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");

    }
}