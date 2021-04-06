using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterAnim
{
    Idle = 0,
    Walking = 1,
    Run = 2,
    Attack = 3,
    Dead = 4,
}

public class PlayerScript : Photon.MonoBehaviour
{
    public CharacterAnim _characterState;

    PhotonView PV;

    public CharacterController MyController;
    public Animator anima;
    public GameObject PlayerCamera;
    public GameObject PlayerLighting;
    public GameObject Ketupat1, Ketupat2;

    public Text PlayerNameText;
    public Text JumlahBerasText;

    public float speed = 6f;
    public float RotationSpeed = 15f;
    float mDesiredRotation = 0f;

    public float berasDimiliki;

    public bool isControlled;
    public bool isSprint;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (isControlled)
        {
            JumlahBerasText.text = "Jumlah Beras : " + berasDimiliki.ToString();
            CheckInput();
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isSprint)
            {
                Run();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _characterState = CharacterAnim.Attack;
            }
        }
        if (_characterState == CharacterAnim.Idle)
        {
            anima.SetFloat("Blend", 0f);
        }
        else if (_characterState == CharacterAnim.Run)
        {
            anima.SetFloat("Blend", 1f);
        }
        else if (_characterState == CharacterAnim.Walking)
        {
            anima.SetFloat("Blend", 0.5f);
        }
        else if (_characterState == CharacterAnim.Attack)
        {
            anima.SetTrigger("Attack");
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
                //anima.SetFloat("Blend", 1f);
                _characterState = CharacterAnim.Run;
                return;
            }
            //anima.SetFloat("Blend", 0.5f);
            _characterState = CharacterAnim.Walking;
        }
        else
        {
            //anima.SetFloat("Blend", 0f);
            _characterState = CharacterAnim.Idle;
        }
    }

    void Run()
    {
        StartCoroutine(EndRun());
    }

    void DestroyOther()
    {
        Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!photonView.isMine)
    //        return;

    //    PhotonView target = other.gameObject.GetComponent<PhotonView>();

    //    if(target != null && (!target.isMine || target.isSceneView))
    //    {
    //        if(target.gameObject.tag == "Beras")
    //        {
    //            PhotonNetwork.Destroy(target.gameObject);
    //            Debug.Log(" Object Destroyed All Client");
    //            GiveScore(1);
    //            Ketupat1.transform.localScale += new Vector3(0.005f , 0.025f, 0.025f);
    //            Ketupat2.transform.localScale += new Vector3(0.005f, 0.025f, 0.025f);
    //        }
    //    }  
    //}

    IEnumerator EndRun()
    {
        isSprint = true;
        speed += 5;
        yield return new WaitForSeconds(5);
        speed -= 5f;
        isSprint = false;
    }

    public void GiveScore(float _amount)
    {
        berasDimiliki += _amount;
        JumlahBerasText.text = "Jumlah Beras : " + berasDimiliki.ToString();

        PV.RPC(nameof(RpcScore), PhotonTargets.AllBuffered, berasDimiliki);
    }

    [PunRPC]
    private void RpcScore(float _currentScore)
    {
        _currentScore = berasDimiliki;
        JumlahBerasText.text = "Jumlah Beras : " + berasDimiliki.ToString();
    }

  
    public void Death()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }
}
