using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Photon.MonoBehaviour , IPunObservable
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
    public GameObject Bush1;
    public GameObject Bush2;
    public GameObject Bush3;
    public GameObject Bush4;

    public GameObject Beras1;
    public GameObject Beras2;
    public GameObject Beras3;

    public int xPos;
    public int zPos;
    public int objectToGenerate;
    public int objectQuantity;
    public int berasQuantity;
    public int berasToGenerate;


    
    private void Awake()
    {
        GameCanvas.SetActive(true);
        if (PhotonNetwork.isMasterClient)
        {
            photonView.RPC("Spawner", PhotonTargets.AllViaServer);
        }
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
        while(objectQuantity < 100)
        {
            objectToGenerate = Random.Range(1, 6);
            xPos = Random.Range(-50,56);
            zPos = Random.Range(-41,67);
            

            if (objectToGenerate == 1)
            {
               PhotonNetwork.Instantiate(Trees1.name, new Vector3(xPos, 0, zPos), Quaternion.identity,0);
            }
            if (objectToGenerate == 2)
            {
                PhotonNetwork.Instantiate(Trees2.name, new Vector3(xPos, 0, zPos), Quaternion.identity,0);
            }
            if (objectToGenerate == 3)
            {
                PhotonNetwork.Instantiate(Bush1.name, new Vector3(xPos, 1, zPos), Quaternion.identity,0);
            }
            if (objectToGenerate == 4)
            {
                PhotonNetwork.Instantiate(Bush2.name, new Vector3(xPos, 1, zPos), Quaternion.identity,0);
            }
            if (objectToGenerate == 5)
            {
                PhotonNetwork.Instantiate(Bush3.name, new Vector3(xPos, 1, zPos), Quaternion.identity,0);
            }
            if (objectToGenerate == 6)
            {
                PhotonNetwork.Instantiate(Bush4.name, new Vector3(xPos, 1, zPos), Quaternion.identity,0);
            }
            yield return new WaitForSeconds(0.1f);
            objectQuantity += 1;
        }
    }

    IEnumerator GenerateBeras()
    {
        while(berasQuantity < 35)
        {
            berasToGenerate = Random.Range(1, 3);
            xPos = Random.Range(-50, 56);
            zPos = Random.Range(-41, 67);

           if (berasToGenerate == 1)
            {
                PhotonNetwork.Instantiate(Beras1.name, new Vector3(xPos, 1, zPos), Quaternion.identity,0);
            }
           if (berasToGenerate == 2)
            {
                PhotonNetwork.Instantiate(Beras2.name, new Vector3(xPos, 1, zPos), Quaternion.identity,0);
            }
           if (berasToGenerate == 3)
            {
                PhotonNetwork.Instantiate(Beras3.name, new Vector3(xPos, 1, zPos), Quaternion.identity,0);
            }
            yield return new WaitForSeconds(0.1f);
            berasQuantity += 1;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream , PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.xPos);
            stream.SendNext(this.zPos);
            stream.SendNext(this.berasQuantity);
            stream.SendNext(this.objectQuantity);
            stream.SendNext(this.berasToGenerate);
            stream.SendNext(this.objectToGenerate);
       
        } 
        else
        {
            xPos = (int)stream.ReceiveNext();
            zPos = (int)stream.ReceiveNext();
            berasQuantity = (int)stream.ReceiveNext();
            objectQuantity = (int)stream.ReceiveNext();
            berasToGenerate = (int)stream.ReceiveNext();
            objectToGenerate = (int)stream.ReceiveNext();

        }

    }
    [PunRPC]
    public void Spawner()
    {
        StartCoroutine(GenerateObjects());
        StartCoroutine(GenerateBeras());
    }

 
}
