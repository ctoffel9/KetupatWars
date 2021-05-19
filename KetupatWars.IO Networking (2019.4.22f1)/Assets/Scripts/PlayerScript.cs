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

    GameManager ManagerController;
    public GameObject DC;
    public GameObject body;

    public CharacterController MyController;
    public KetupatScript KetupatScript;
    public Animator anima;
    public GameObject PlayerCamera;
    public GameObject PlayerLighting;
    public GameObject KetupatBack;
    public GameObject KetupatAttack;
    public GameObject VictoryPanel;

    public Renderer[] renderer;

    public Text PlayerNameText;
    public Text JumlahBerasText;

    public float speed = 6f;
    public float RotationSpeed = 15f;
    float mDesiredRotation = 0f;

    public float berasDimiliki;

    public bool isControlled;
    public bool isSprint;
    public bool isAttacking;

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
            if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
            {
                StartCoroutine(KetupatSummon());
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

        RpcScore(berasDimiliki);
        RpcWin();
        ChangeColor();
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

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.isMine)
            return;

        PhotonView target = other.gameObject.GetComponent<PhotonView>();

        if(target != null && (!target.isMine || target.isSceneView))
       {
            if(target.gameObject.tag == "Beras")
            {
                PhotonNetwork.Destroy(target.gameObject);
                Debug.Log(" Object Destroyed All Client");
                GiveScore(1);
                KetupatAttack.transform.localScale += new Vector3(0.005f , 0.025f, 0.025f);
                KetupatBack.transform.localScale += new Vector3(0.005f, 0.025f, 0.025f);
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
    IEnumerator KetupatSummon()
    {
        isAttacking=(true);
        KetupatAttack.SetActive(true);
        KetupatBack.SetActive(false);
        _characterState = CharacterAnim.Attack;
        yield return new WaitForSeconds(1);
        KetupatAttack.SetActive(false);
        KetupatBack.SetActive(true);
        new WaitForSeconds(2);
        isAttacking = (false);
    }

    /*IEnumerator DeathSeq()
    {
        body.SetActive(false);
        DC.SetActive(true);
        yield return new WaitForSeconds(10);
        PhotonNetwork.Destroy(this.gameObject);
        Debug.Log("Berhasil");       
    }*/


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

    [PunRPC]
    void RpcKetupat()
    {
        StartCoroutine(KetupatSummon());
    }
    
    public void Death()
    {

        //StartCoroutine(DeathSeq());
    }
    [PunRPC]
    public void RpcWin()
    {
        if(berasDimiliki == 50)
        {
            PhotonNetwork.DestroyAll();
            VictoryPanel.SetActive(true);
            
        }
    }
    public void ChangeColor()
    {
        if(berasDimiliki >= 5 && berasDimiliki <= 15)
        {
            renderer[0].material.color = Color.red;
            renderer[1].material.color = Color.red;
        }
        if(berasDimiliki >= 15 && berasDimiliki <= 25)
        {
            renderer[0].material.color = Color.blue;
            renderer[1].material.color = Color.blue;
        }
        if(berasDimiliki >= 25 && berasDimiliki <= 35)
        {
            renderer[0].material.color = Color.black;
            renderer[1].material.color = Color.black;
        }
    }
}
