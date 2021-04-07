using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform tanganKanan;
    [SerializeField]
    private Transform wieldKetupat;

    public CharacterController controller;
    public Animator anima;
    public GameObject ketupat;
    public float speed = 6f;
    public float RotationSpeed = 15f;
    public int jumlahBeras;

    float mDesiredRotation = 0f;

    private bool isSprint = false;

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
              
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSprint)
        {
            Run();
        }
        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
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
    void Attack()
    {
        ketupat.transform.parent = tanganKanan;
        ketupat.transform.position = tanganKanan.position;
        anima.SetTrigger("Attack");
    }
    void Run()
    {
        StartCoroutine(EndRun());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collectible")
        {
            Destroy(other.gameObject);
            ketupat.transform.localScale += new Vector3(0.005f, 0.025f, 0.025f);
            jumlahBeras++;
        }
    }
    
    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(2);
        ketupat.transform.parent = wieldKetupat;
        ketupat.transform.position = wieldKetupat.position;
        ketupat.transform.rotation = wieldKetupat.rotation;
    }

    IEnumerator EndRun()
    {
        isSprint = true;
        speed += 5f;
        yield return new WaitForSeconds(5);
        speed -= 5f;
        isSprint = false;
    }
}
