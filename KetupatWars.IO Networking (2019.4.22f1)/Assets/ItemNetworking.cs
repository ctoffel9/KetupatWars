using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNetworking : Photon.MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerScript>())
        {
            other.GetComponent<PlayerScript>().GiveScore(1f);
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.Destroy(this.gameObject);
                Debug.Log("Player destroyed");
            }
        }
    }
}
