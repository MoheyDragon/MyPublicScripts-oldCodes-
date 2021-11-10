using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CharecterController : MonoBehaviour
{
    Animator anime;
    CharacterController controller;
    public static int ControlMethode; public static bool cutscene = false; public static int Level;
    Vector3 velocity;
    [HideInInspector]
    public Vector3 CheckPoint;
    float MoveSpeed = 5; float JumpPower = 5; float gravity = -20f;
    int JumpState=0; int Power = 1000;int Tries = 3;
    bool CutLock = false;
    TextMeshProUGUI JumpstateUi,PowerUi,TriesUi;
    GameObject ImgJump, ImgPower, ImgTry; 
    int CheckPower;
    Transform GroundCheck;float groundDistance = .4f;LayerMask groundMask;bool IsGrounded;
    [HideInInspector]
    public float RotateSpeed = 100;
    [HideInInspector]
    public GameObject BulidingParent,Fog;
    public AK.Wwise.Event footstep, jump, jumpHigh,land, CheckPointEvent,Respwan,Lose,JumpChange;
    Menu menu;
    // Start is called before the first frame update
    private void Awake()
    {
            ImgJump = GameObject.FindGameObjectWithTag("JumpState");
            ImgPower = GameObject.FindGameObjectWithTag("Power");
            ImgTry = GameObject.FindGameObjectWithTag("Tries");
            JumpstateUi = ImgJump.GetComponentInChildren<TextMeshProUGUI>();
            PowerUi = ImgPower.GetComponentInChildren<TextMeshProUGUI>();
            TriesUi = ImgTry.GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        CheckPoint = transform.position;CheckPower = Power;
        anime = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        foreach (Transform child in transform)
        {
            if (GroundCheck == null)
            {
                if (child.tag == "GroundCheck")
                    GroundCheck = child;
            }
            
        }
        groundMask= LayerMask.GetMask("Ground");
        if (Level==6)
        {
            Power = 0;
        }
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        Fog = GameObject.FindGameObjectWithTag("Fog");
        StartCoroutine(Fix());
    }
    void Update()
    { 
        if (!Menu.Pause)
        {
            
            //gravity physics
            IsGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
            if (IsGrounded && velocity.y < 0)
            {
                velocity.y = 0;
                anime.SetBool("IsJumping", false);
                anime.SetBool("IsGrounded", true);
            }
            if (IsGrounded && BulidingParent != null) transform.parent = BulidingParent.transform;
            if (!IsGrounded)
            {
                anime.SetBool("IsGrounded", false);
                transform.SetParent(null);
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            if (!cutscene)
            {
                //revive UI after cutscene
                if (!CutLock && Level > 4 && Level != 6)
                {
                    ImgJump.SetActive(true);
                    ImgPower.SetActive(true);
                    ImgTry.SetActive(true);
                    CutLock = true;
                }

                //JumpState Switcher
                if ((Level > 0))
                {
                    if (Input.GetKeyDown(KeyCode.BackQuote) && Level > 2) JumpSwitcher(0);
                    if (Input.GetKeyDown(KeyCode.Alpha1)) JumpSwitcher(1);
                    if (Input.GetKeyDown(KeyCode.Alpha2) && Level > 1) JumpSwitcher(2);
                    if (Input.GetKeyDown(KeyCode.Alpha3) && Level > 2) JumpSwitcher(3);
                    if (Input.GetKeyDown(KeyCode.Alpha4) && Level > 2) JumpSwitcher(4);
                    if (Input.GetKeyDown(KeyCode.Tab) && Level > 2)
                    {
                        if (JumpState <= 3) JumpSwitcher(JumpState + 1);
                        else JumpSwitcher(0);
                    }
                }
                //MoveForward,Backward
                float vert = Input.GetAxisRaw("Vertical");
                Vector3 MoveDirection = new Vector3(0, 0, vert);
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {

                    MoveDirection = transform.TransformDirection(MoveDirection);
                    controller.Move(MoveDirection * MoveSpeed * Time.deltaTime);
                    if (Input.GetKey(KeyCode.W)) anime.SetBool("IsRunning", true);
                    else anime.SetBool("Backward", true);
                }
                if (Input.GetKeyUp(KeyCode.W)) anime.SetBool("IsRunning", false);
                if (Input.GetKeyUp(KeyCode.S)) anime.SetBool("Backward", false);


                if (ControlMethode == 1)
                {
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                        transform.Rotate(0, Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime, 0);
                }
                if (ControlMethode == 2)
                {
                    float Horz = Input.GetAxisRaw("Horizontal");
                    Vector3 MoveDirectionHorizontal = new Vector3(Horz, 0, 0);
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                    {

                        MoveDirectionHorizontal = transform.TransformDirection(MoveDirectionHorizontal);
                        controller.Move(MoveDirectionHorizontal * MoveSpeed * Time.deltaTime);
                        if (Input.GetKey(KeyCode.A)) anime.SetBool("Left", true);
                        else anime.SetBool("Right", true);
                    }
                }
                if (Input.GetKeyUp(KeyCode.A)) anime.SetBool("Left", false);
                if (Input.GetKeyUp(KeyCode.D)) anime.SetBool("Right", false);



                //Jump
                if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
                {
                    anime.SetBool("IsJumping", true);
                    velocity.y = Mathf.Sqrt(JumpPower * -2f * gravity);
                }
                //ChangeControlMethode
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (ControlMethode == 1)
                    {
                        ControlMethode = 2;
                        Camera.main.GetComponent<Camera_Controller>().Control2();
                        anime.SetBool("Left", false); anime.SetBool("Right", false);
                    }
                    else ControlMethode = 1;
                }
            }
            //Cutscene
            if (cutscene && Level > 4)
            {
                ImgJump.SetActive(false);
                ImgPower.SetActive(false);
                ImgTry.SetActive(false);
                CutLock = false;
            }
        }

        
        
    }
    void JumpSwitcher(int state)
    {
            JumpState = state;
            if (state == 0) { MoveSpeed = 5; JumpPower = 5; RotateSpeed = 100; Camera_Controller.CurrentZoom = 2; }
            if (state == 1) { MoveSpeed = 5; JumpPower = 10; RotateSpeed = 100; Camera_Controller.CurrentZoom = 3; }
            if (state == 2) { MoveSpeed = 10; JumpPower = 15; RotateSpeed = 200; Camera_Controller.CurrentZoom = 4; }
            if (state == 3) { MoveSpeed = 10; JumpPower = 20; RotateSpeed = 300; Camera_Controller.CurrentZoom = 5; }
            if (state == 4) { MoveSpeed = 20; JumpPower = 30; RotateSpeed = 400; Camera_Controller.CurrentZoom = 6;}
            JumpstateUi.text = JumpState.ToString();
            JumpChange.Post(gameObject);
    }
    public void PowerUsage(int type)
    {
        if (Level>=3&&Level!=6)
        {
            //powerUse
            if (type == 1)Power -= JumpState;
            if (type == 2)Power -= JumpState * 25;
            //powerUps
            if (type == 4) Power += 50;
            if (type == 5) Power += 200;
            if (type == 6) Power += 500;
            PowerUi.text = Power.ToString();
            if (Power<=0)
            {
                if (Level > 4)
                {
                    Tries--;
                    TriesUi.text = Tries.ToString();
                    if (Tries == 0)
                    {
                        Lose.Post(gameObject);
                        menu.stopAll.Post(menu.gameObject);
                        foreach (GameObject game in GameObject.FindGameObjectsWithTag("MovingHouse"))
                        {
                            menu.stopAll.Post(game);
                        }
                        menu.GameOver();
                    }
                    else
                        returnToLastCheckPoint();
                }
            }
        }
        else if (Level == 6)
        {
            //powerUse
            if (type == 1) Power += JumpState;
            if (type == 2) Power += JumpState * 25;
            if (type == 3) Power += JumpState * 10;
            PowerUi.text = Power.ToString();
        }
        if (type == 1) footstep.Post(gameObject);
        else if (type == 2)
        {
            anime.SetBool("IsJumping", false);
            if (JumpState >= 3)
                jumpHigh.Post(gameObject);
            else jump.Post(gameObject);
        }
        else if (type == 3)
            land.Post(gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag== "CheckPoint")
        {
            CheckPointEvent.Post(gameObject);
            CheckPoint = new Vector3(other.transform.position.x, other.transform.position.y+2, other.transform.position.z);
            CheckPower = Power;
            Destroy(other.gameObject);
            foreach (GameObject cube in GameObject.FindGameObjectsWithTag("PowerUp"))
            {
                cube.GetComponent<PowerFiller>().Destroy();
            }
            foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                zombie.GetComponent<EnemyAi>().Destroy();
            }

        }
        if (other.tag=="Fog")
        {
            jump.Stop(gameObject);
            if (Level > 4)
            {
                Tries--;
                TriesUi.text = Tries.ToString();
                if (Tries == 0)
                {
                    Lose.Post(gameObject);
                    menu.stopAll.Post(menu.gameObject);
                    foreach (GameObject game in GameObject.FindGameObjectsWithTag("MovingHouse"))
                    {
                        menu.stopAll.Post(game);
                    }
                    menu.GameOver();
                }
                else
                    returnToLastCheckPoint();
            }
            else
                returnToLastCheckPoint();
        }
    }
     public void returnToLastCheckPoint()
    {
        this.gameObject.SetActive(false);
        if (Level>3)
        {
            Power = CheckPower;
            PowerUi.text = Power.ToString();
        }
        jumpHigh.Stop(gameObject);
        BulidingParent = null;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<EnemyAi>().Rebirth();
            enemy.GetComponent<EnemyAi>().Chase(false);
        }
        foreach (GameObject cube in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            cube.GetComponent<PowerFiller>().Rebirth();
        }
        SceneManger2.CubsCheck = 0;
        Lose.Post(gameObject);
        Fog.GetComponent<Fog>().Return(gameObject);
    }

    public void Pause(bool State)
    {
        if (State==true)
        {
            controller.enabled = false;
            anime.speed = 0;
        }
        else
        {
            controller.enabled = true;
            anime.speed = 1;
        }
    }
    public void CutScene(bool state)
    {
        ImgTry.gameObject.SetActive(!state);
        ImgJump.gameObject.SetActive(!state);
        ImgPower.gameObject.SetActive(!state);
        PowerUi.gameObject.SetActive(!state);
        TriesUi.gameObject.SetActive(!state);
        JumpstateUi.gameObject.SetActive(!state);
    }
    public IEnumerator Fix()
    {
        yield return new WaitForSeconds(0.01f);
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            ControlMethode = SaveSystem.Load().Methode;
            if (ControlMethode == 2)
                Camera.main.GetComponent<Camera_Controller>().Control2();
        }
        else ControlMethode = 1;
    }
    public void Respawn()
    {
        Respwan.Post(gameObject);
        transform.position = CheckPoint;
    }
}
