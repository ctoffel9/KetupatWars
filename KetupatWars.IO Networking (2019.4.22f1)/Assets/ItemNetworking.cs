using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNetworking : Photon.MonoBehaviour
{
    public float skorberas;
    public bool berasputih;
    public bool berasmerah;
    public bool beraskuning;
    PlayerScript playerData;
    // Start is called before the first frame update
    void Start()
    {
        if(berasputih)
        {
            skorberas = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerScript>())
        {
            
            photonView.RPC(nameof(RpcItem), PhotonTargets.All);
            
        }
    }
    [PunRPC]
    public void RpcItem()
    {
        Destroy(this.gameObject);
    }
}
