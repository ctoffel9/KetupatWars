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

    public GameObject Trees1;
    public GameObject Trees2;

    public int xPos;
    public int zPos;
    public int objectToGenerate;
    public int objectQuantity;


    private void Awake()
    {
        GameCanvas.SetActive(true);
        StartCoroutine(GenerateObjects());
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

    public void TreesSpawner()
    {
        int randomValueX = Random.RandomRange(-240,340);
        int randomValueZ = Random.RandomRange(-203,295);


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

    IEnumerator GenerateObjects()
    {
        while(objectQuantity < 150)
        {
            objectToGenerate = Random.RandomRange(1, 2);
            xPos = Random.Range(-186,197);
            zPos = Random.Range(-185,196);

            if (objectToGenerate == 1)
            {
                Instantiate(Trees1, new Vector3(xPos, 0, zPos), Quaternion.identity);
            }
            if (objectToGenerate == 2)
            {
                Instantiate(Trees2, new Vector3(xPos, 0, zPos), Quaternion.identity);
            }
            yield return new WaitForSeconds(0.1f);
            objectQuantity += 1;
        }
    }
}
