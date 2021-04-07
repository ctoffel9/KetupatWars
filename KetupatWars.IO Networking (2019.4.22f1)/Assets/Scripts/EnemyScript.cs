using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public Animator anim;
    public int jumlahBeras = 30;
    public GameObject nasi;
    public Transform dropArea;

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
        if(other.gameObject.tag == "Ketupat")
        {
            StartCoroutine(destroy(this.gameObject));
        }
    }
    IEnumerator destroy (GameObject enemy)
    {
        Debug.Log("Animasi Dead dipanggil");
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(2);
        Destroy(enemy);
        dropItem();
    }

    void dropItem()
    {
        
        for (int i = 0; i < jumlahBeras; i++)
        {
            GameObject beras = Instantiate(nasi, transform.position ,Quaternion.identity);
        }
    }

}
