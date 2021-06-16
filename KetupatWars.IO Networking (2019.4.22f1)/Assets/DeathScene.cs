using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScene : MonoBehaviour
{
    [SerializeField] private GameObject DeadInstance;
    [SerializeField] private PlayerScript playerController;
    [SerializeField] private GameManager gameController;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.SetManager(GetComponent<DeathScene>());

        gameController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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

    public void Respawn()
    {
        
        gameController.GameCanvas.SetActive(true);
        Destroy(this.gameObject);
        Debug.Log("testing");

    }
}