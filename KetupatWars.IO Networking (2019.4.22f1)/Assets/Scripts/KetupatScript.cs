using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetupatScript : Photon.MonoBehaviour , IPunObservable
{
    PlayerScript controllerScript;
    PlayerNetworking networkingScript;

    // GameObject DeathCanvas;
    // Start is called before the first frame update
    void Start()
    {
        //DeathCanvas = GameObject.Find("GameManager").GetComponent<GameManager>().DeathCanvas;       

    }

    // Update is called once per frame
    void Update()
    {  
        
    }

   
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerScript>().isControlled == false) 
        {
            PhotonNetwork.Destroy(other.gameObject);
            Debug.Log("Player destroyed");
            //DeathCanvas.SetActive(true);
        } 
        else if (photonView.isMine && this.gameObject.GetComponent<PlayerScript>().isControlled == true)
        {
            PhotonNetwork.Destroy(this.gameObject);
            Debug.Log("You Are Dead");
        }
        
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
          

        }
        else
        {

        }

    }

    public void RpcDeath()
    {
      
    }
}
