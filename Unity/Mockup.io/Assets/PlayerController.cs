using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Animator anima;
    public float speed = 6f;
    public float RotationSpeed = 15f;

    float mDesiredRotation = 0f;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0)
        {
            mDesiredRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
           
        }
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation  = Quaternion.Euler(0, mDesiredRotation, 0);
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Time.deltaTime);


       if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
            anima.SetFloat("Blend", 0.5f);
        }
       else
        {
            anima.SetFloat("Blend", 0f);
        }
    }
}

