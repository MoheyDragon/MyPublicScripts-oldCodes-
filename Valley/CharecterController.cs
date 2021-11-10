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
    Vector3 velocity;
    [HideInInspector]
    public Vector3 CheckPoint;
    public float MoveSpeed = 5; public float RotateSpeed = 100; public float JumpPower = 5;public float gravity = -20f;
    int souls = 3;
    Transform GroundCheck;float groundDistance = .4f;LayerMask groundMask;bool IsGrounded;
    public bool CombatLock,IsCombating;
    public AK.Wwise.Event Music,footstep, HitTree, HitEnemy, HitHero, treeDrop,collect,build;
    //public AK.Wwise.Event footstep, jump, jumpHigh,land, CheckPointEvent,Respwan,Lose,JumpChange;
    void Start()
    {
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
        //menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        Music.Post(gameObject);
    }
    void Update()
    { 
            //gravity physics
            IsGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
            if (IsGrounded && velocity.y < 0)
            {
                velocity.y = 0;
                anime.SetBool("IsJumping", false);
                anime.SetBool("IsGrounded", true);
            }
                
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        //MoveForward,Backward
        if (!CombatLock)
        {
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
            float Horz = Input.GetAxisRaw("Horizontal");
            Vector3 MoveDirectionHorizontal = new Vector3(Horz, 0, 0);
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {

                MoveDirectionHorizontal = transform.TransformDirection(MoveDirectionHorizontal);
                controller.Move(MoveDirectionHorizontal * MoveSpeed * Time.deltaTime);
                if (Input.GetKey(KeyCode.A)) anime.SetBool("Left", true);
                else anime.SetBool("Right", true);
            }
            if (Input.GetKeyUp(KeyCode.A)) anime.SetBool("Left", false);
            if (Input.GetKeyUp(KeyCode.D)) anime.SetBool("Right", false);

        }



        //Jump
        //if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        //{
        //    anime.SetBool("IsJumping", true);
        //    anime.SetBool("IsGrounded", false);
        //    anime.Play("Unarmed-Jump 1");
        //    velocity.y = Mathf.Sqrt(JumpPower * -2f * gravity);
        //}
    }
    public void PowerUsage(int type)
    {

        //if (type == 1) footstep.Post(gameObject);
        if (type==1)
        footstep.Post(gameObject);
        if (type == 2)
        {
            anime.SetBool("IsJumping", false);
            anime.SetBool("IsGrounded", true);
            //jump.Post(gameObject);
        }
        else if (type == 3)
        {//land.Post(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        //if (other.tag== "CheckPoint")
        //{
        //    CheckPointEvent.Post(gameObject);
        //    CheckPoint = new Vector3(other.transform.position.x, other.transform.position.y+2, other.transform.position.z);
        //    CheckPower = Power;
        //    Destroy(other.gameObject);
        //    foreach (GameObject cube in GameObject.FindGameObjectsWithTag("PowerUp"))
        //    {
        //        cube.GetComponent<PowerFiller>().Destroy();
        //    }
        //    foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Enemy"))
        //    {
        //        zombie.GetComponent<EnemyAi>().Destroy();
        //    }

        //}
        //if (other.tag=="Fog")
        //{
        //    jump.Stop(gameObject);
        //    if (Level > 4)
        //    {
        //        souls--;
        //        TriesUi.text = souls.ToString();
        //        if (souls == 0)
        //        {
        //            Lose.Post(gameObject);
        //            menu.stopAll.Post(menu.gameObject);
        //            foreach (GameObject game in GameObject.FindGameObjectsWithTag("MovingHouse"))
        //            {
        //                menu.stopAll.Post(game);
        //            }
        //            menu.GameOver();
        //        }
        //        else
        //            returnToLastCheckPoint();
        //    }
        //    else
        //        returnToLastCheckPoint();
        //}
    }
    // public void returnToLastCheckPoint()
    //{
    //    this.gameObject.SetActive(false);
    //    if (Level>3)
    //    {
    //        Power = CheckPower;
    //        PowerUi.text = Power.ToString();
    //    }
    //    jumpHigh.Stop(gameObject);
    //    BulidingParent = null;
    //    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
    //    {
    //        enemy.GetComponent<EnemyAi>().Rebirth();
    //        enemy.GetComponent<EnemyAi>().Chase(false);
    //    }
    //    foreach (GameObject cube in GameObject.FindGameObjectsWithTag("PowerUp"))
    //    {
    //        cube.GetComponent<PowerFiller>().Rebirth();
    //    }
    //    SceneManger2.CubsCheck = 0;
    //    Lose.Post(gameObject);
    //    Fog.GetComponent<Fog>().Return(gameObject);
    //}


    //public void Respawn()
    //{
    //    Respwan.Post(gameObject);
    //    transform.position = CheckPoint;
    //}
}
