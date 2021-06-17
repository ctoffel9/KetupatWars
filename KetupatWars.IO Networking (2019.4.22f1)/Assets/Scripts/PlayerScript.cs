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
    [SerializeField]private DeathScene DeathController;
    [SerializeField]private GameManager gameController;

    public AudioSource audiodata;
    public AudioClip spin;
    public AudioClip hitted;
    public AudioClip walk;
    public AudioClip run;
    public AudioClip att;

    private GameObject DCInstance;
    private GameObject VCInstance;

    public GameObject DeathCanvas;
    public GameObject Player;

    public CharacterController MyController;
    public KetupatScript KetupatScript;
    public Animator anima;
    public GameObject PlayerCamera;
    public GameObject PlayerLighting;
    public GameObject KetupatBack;
    public GameObject KetupatAttack;
    public GameObject VictoryPanel;
    public GameObject DataCanvas;
    public GameObject Beras;

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
    public bool isWin;
    public bool isLose;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        
    }

    void Start()
    {
        DCInstance = Instantiate(DeathCanvas);
        DeathScene dead = GameObject.FindGameObjectWithTag("DeadUI").GetComponent<DeathScene>();
        dead.SetManager(GetComponent<PlayerScript>());

        DeathController = GameObject.FindGameObjectWithTag("DeadUI").GetComponent<DeathScene>();

        gameController = GetComponent<GameManager>();
        if (PV.isMine)
        {

        photonView.RPC(nameof(RPCStart), PhotonTargets.AllBuffered);
        }
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
                photonView.RPC("RpcKetupat", PhotonTargets.All);
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

        //RpcWin();
        ChangeColor();

        if (PV.isMine)
        {
            if (berasDimiliki >= 200)
            {
                isWin = true;
                gameController.EndGame();
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
                _characterState = CharacterAnim.Run;
                return;
            }
            _characterState = CharacterAnim.Walking;
        }
        else
        {
        
            _characterState = CharacterAnim.Idle;
        }
    }

  

    void Run()
    {
        StartCoroutine(EndRun());
    }

    public void WinLoseCon ()
    {
        if (PV.isMine)
        {
            if(isWin)
            {
                Debug.Log("Iswin");
                PhotonNetwork.LoadLevel("WinScene");
            }
            else
            {
                Debug.Log("Ilose");
                PhotonNetwork.LoadLevel("LoseScene");
            }
        }
    } 

    void SfxManager()
    {
        if (Input.GetKeyDown(KeyCode.W | KeyCode.A | KeyCode.S | KeyCode.D) | isSprint == false )
        {
            audiodata.Play();
        }
        else if (Input.GetKeyUp(KeyCode.W | KeyCode.A | KeyCode.S | KeyCode.D) | isSprint == false)
        {
            audiodata.Stop();
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (PV.isMine)
        {
            ItemNetworking item = other.GetComponent<ItemNetworking>();
            if (item)
            {
                GiveScore(item.skorberas);
                KetupatAttack.transform.localScale += new Vector3(0.005f, 0.0050f, 0.0050f);
                KetupatBack.transform.localScale += new Vector3(0.005f, 0.0050f, 0.0050f);
            }

            //KetupatScript ketupat = other.GetComponent<KetupatScript>();
            //if (ketupat)
            //{
                
            //}
        }
        // if (!photonView.isMine)
        //     return;

        // PhotonView target = other.gameObject.GetComponent<PhotonView>();

        // if(target != null && (!target.isMine || target.isSceneView))
        //{
        //     if (target.gameObject.tag == "Ketupat")
        //     {

        //     }
        // }    
    }

    IEnumerator EndRun()
    {
        isSprint = true;
        speed += 5;
        audiodata.PlayOneShot(run);
        yield return new WaitForSeconds(5);
        audiodata.Stop();
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

    public void GiveScore(float _amount)
    {
        JumlahBerasText.text = "Jumlah Beras : " + berasDimiliki.ToString();

        PV.RPC(nameof(RpcScore), PhotonTargets.AllBuffered, _amount);
    }

    [PunRPC]
    private void RpcScore( float _amount)
    {
        berasDimiliki += _amount;
        JumlahBerasText.text = "Jumlah Beras : " + berasDimiliki.ToString();
    }

    [PunRPC]
    void RpcKetupat()
    {
        StartCoroutine(KetupatSummon());
    }
    [PunRPC]
    public void RpcDeath()
    {
        if (PV.isMine)
        {
            audiodata.PlayOneShot(hitted);
            DeathController.DeathS();
        }
        StartCoroutine(DeathScene());      
    }

    IEnumerator DeathScene()
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
    
    public void Drop()
    {
        for (int i = 0; i < berasDimiliki; i++)
        {
            GameObject beras = PhotonNetwork.Instantiate(Beras.name, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity, 0);
            Debug.Log("berasdropbro");
        }
        //photonView.RPC(nameof(RpcDrop), PhotonTargets.All);
    }
    //[PunRPC]
    //public void RpcDrop()
    //{
    //    if (PV.isMine)
    //    {
    //        Debug.Log("rpcdropdijalankan1");
    //        photonView.RPC(nameof(RpcDropBeras), PhotonTargets.All);
    //    }
    //}
    //[PunRPC]
    //public void RpcDropBeras()
    //{
    //    Debug.Log("rpcdropdijalankan2");
    //for (int i = 0; i < berasDimiliki; i++)
    //    {
    //        GameObject beras = PhotonNetwork.Instantiate(Beras.name, new Vector3(transform.position.x,0.5f,transform.position.z), Quaternion.identity, 0);
    //        Debug.Log("berasdropbro");
    //    }
    //}
    public void Death()
    {
        
        photonView.RPC("RpcDeath", PhotonTargets.All);
    }

    [PunRPC]
    public void RpcWin()
    {
        if(berasDimiliki == 50)
        {
            PhotonNetwork.DestroyAll();
            
            
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

    [PunRPC]
    public void RPCStart()
    {
        
        gameController = FindObjectOfType<GameManager>();
        gameController.players.Add(this);
        gameController.jumlahplayer += 1;
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.isWriting)
    //    {
    //       stream.SendNext(this.berasDimiliki);
           

    //    }
    //    else
    //    {
    //        beras = (int)stream.ReceiveNext();
          
    //    }

    //}
}
