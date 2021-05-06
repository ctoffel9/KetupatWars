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
        int randomValueZ = Random.Range(19, -40);
        int randomValueX = Random.Range(35, -40);

        GameObject Player = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(randomValueX, -1 , randomValueZ), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        DeathCanvas.SetActive(false);
    }

    public void SpawnPlayer2()
    {
        int randomValueZ = Random.Range(19, -40);
        int randomValueX = Random.Range(35, -40);

        GameObject Player = PhotonNetwork.Instantiate(PlayerPrefab2.name, new Vector3(randomValueX, -1, randomValueZ), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        DeathCanvas.SetActive(false);
    }

    public void SpawnPlayer3()
    {
        int randomValueZ = Random.Range(19, -40);
        int randomValueX = Random.Range(35, -40);

        GameObject Player = PhotonNetwork.Instantiate(PlayerPrefab3.name, new Vector3(randomValueX, -1, randomValueZ), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        DeathCanvas.SetActive(false);
    }

    public void SpawnPlayer4()
    {
        int randomValueZ = Random.Range(19, -40);
        int randomValueX = Random.Range(35, -40);

        GameObject Player = PhotonNetwork.Instantiate(PlayerPrefab4.name, new Vector3(randomValueX, -1, randomValueZ), Quaternion.identity, 0);
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
        GameCanvas.SetActive(true);
        DeathCanvas.SetActive(false);
    }
}
