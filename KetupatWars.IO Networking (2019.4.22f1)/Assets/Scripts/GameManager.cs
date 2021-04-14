using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject PlayerPrefab2;
    public GameObject PlayerPrefab3;
    public GameObject PlayerPrefab4;
    public GameObject GameCanvas;
    public GameObject SceneCamera;
    public GameObject DeathCanvas;

    private void Awake()
    {
        GameCanvas.SetActive(true);
    }

    public void SpawnPlayer()
    {
        float randomValue = Random.Range(0f, 0f);

        GameObject Player = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(this.transform.position.x * randomValue, -1 , this.transform.position.z * randomValue), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        DeathCanvas.SetActive(false);
    }

    public void SpawnPlayer2()
    {
        float randomValue = Random.Range(0f, 0f);

        GameObject Player = PhotonNetwork.Instantiate(PlayerPrefab2.name, new Vector3(this.transform.position.x * randomValue, -1, this.transform.position.z * randomValue), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        DeathCanvas.SetActive(false);
    }

    public void SpawnPlayer3()
    {
        float randomValue = Random.Range(0f, 0f);

        GameObject Player = PhotonNetwork.Instantiate(PlayerPrefab3.name, new Vector3(this.transform.position.x * randomValue, -1, this.transform.position.z * randomValue), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        DeathCanvas.SetActive(false);
    }

    public void SpawnPlayer4()
    {
        float randomValue = Random.Range(0f, 0f);

        GameObject Player = PhotonNetwork.Instantiate(PlayerPrefab4.name, new Vector3(this.transform.position.x * randomValue, -1, this.transform.position.z * randomValue), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        DeathCanvas.SetActive(false);
    }


    public void DeathMenuOpen()
    {
        DeathCanvas.SetActive(true);
    }
    public void BackToMenu()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }
    public void Respawn()
    {
        SpawnPlayer();
    }
}
