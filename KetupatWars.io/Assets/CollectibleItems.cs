using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAround(this.gameObject, Vector3.up, 360, 2f).setLoopClamp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
