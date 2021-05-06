using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetupatScript : MonoBehaviour
{
    PlayerScript controllerScript;

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
        if (other.GetComponent<PlayerScript>())
        {
            other.GetComponent<PlayerScript>().Death();
            Debug.Log("Player destroyed");
            //DeathCanvas.SetActive(true);
        }
    }

    private void ChangeColor()
    {

    }
}
