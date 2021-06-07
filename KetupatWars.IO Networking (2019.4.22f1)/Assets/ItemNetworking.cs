using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNetworking : Photon.MonoBehaviour
{
    PlayerScript playerData;
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
            other.GetComponent<PlayerScript>().KetupatAttack.transform.localScale += new Vector3(0.005f, 0.0050f, 0.0050f);
            other.GetComponent<PlayerScript>().KetupatBack.transform.localScale += new Vector3(0.005f, 0.0050f, 0.0050f);
            photonView.RPC(nameof(RpcItem), PhotonTargets.All);
            
        }
    }
    [PunRPC]
    public void RpcItem()
    {
        Destroy(this.gameObject);
    }
}
