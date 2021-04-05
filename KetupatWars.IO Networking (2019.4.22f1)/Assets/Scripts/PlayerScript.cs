using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : Photon.MonoBehaviour
{
    
    public CharacterController MyController;
    public Animator anima;
    public GameObject PlayerCamera;
    public GameObject PlayerLighting;
    public Text PlayerNameText;

    public float speed = 6f;
    public float RotationSpeed = 15f;
    float mDesiredRotation = 0f;

    public bool isControlled;
    public bool isSprint;

  
    private void Update()
    {          
        if (isControlled)
        {
            CheckInput();
            if(Input.GetKeyDown(KeyCode.LeftShift) && !isSprint)
            {
                Run();
            }
        }
    }

    private void CheckInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0)
        {
            mDesiredRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        }
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            MyController.Move(direction * speed * Time.deltaTime);
            if (isSprint == true)
            {
                anima.SetFloat("Blend", 1f);
                return;
            }
            anima.SetFloat("Blend", 0.5f);
        }
        else
        {
            anima.SetFloat("Blend", 0f);
        }
    }

    void Run()
    {
        StartCoroutine(EndRun());
    }

    void  DestroyOther()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.isMine)
            return;

        PhotonView target = other.gameObject.GetComponent<PhotonView>();

        if(target != null && (!target.isMine || target.isSceneView))
        {
            if(other.gameObject.tag == "Beras")
            {
                PhotonNetwork.Destroy(other.gameObject);
            }
        }  
    }

    IEnumerator EndRun()
    {
        isSprint = true;
        speed += 5;
        yield return new WaitForSeconds(5);
        speed -= 5f;
        isSprint = false;
    }
}
