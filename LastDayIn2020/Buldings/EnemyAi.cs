using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    Animator anime;
    Rigidbody rg; CharacterController controller;
    float MoveSpeed = 2.5f;
    float gravity = -9.81f; Vector3 velocity;
    Transform GroundCheck; float groundDistance = .4f; LayerMask groundMask; bool IsGrounded;

    float Distance;  float Damping = 6;float PushRange = 2; Transform Target;bool ChaseBool = false;
    Vector3 MoveDirection = new Vector3(0, 0, 0);
    Vector3 StartPos;
    [HideInInspector]
    public bool Lock=false;
    public AK.Wwise.Event EnemyStep,ZombieAlert;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        rg = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        foreach (Transform child in transform)
        {
            if (GroundCheck == null)
            {
                if (child.tag == "GroundCheck")
                    GroundCheck = child;
            }
        }
        groundMask = LayerMask.GetMask("Ground");
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Menu.Pause)
        {
            anime.speed = 0;
            controller.enabled = false;
        }
        if (!Menu.Pause)
        {
            anime.speed = 1;
            controller.enabled = true;
        }
        Distance = Vector3.Distance(new Vector3(Target.position.x,transform.position.y,Target.position.z), transform.position);
        IsGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        if (IsGrounded && velocity.y < 0)
        {
            velocity.y = 0;
            anime.SetBool("IsGrounded", true);
            anime.SetBool("IsJumping", false);
        }
        else if (!IsGrounded)
        {
            anime.SetBool("IsGrounded", false);
        }
           if (ChaseBool&&!Menu.Pause)
        {
            anime.SetBool("IsRunning", true);
            Quaternion rotation = Quaternion.LookRotation(new Vector3(Target.position.x, transform.position.y, Target.position.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
            MoveDirection = transform.forward*MoveSpeed;
            controller.Move(MoveDirection * Time.deltaTime);
            if (Distance<PushRange)
                rg.AddForce(MoveDirection*10, ForceMode.Impulse);
        }
        else if ( !ChaseBool)
        {
            anime.SetBool("IsRunning", false);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void Chase(bool Value)
    {
        ChaseBool = Value;
        if (Value)
            ZombieAlert.Post(gameObject);
            
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Fog")
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
                Chase(false);
                Lock = true;
            }
        }
    }
    public void Rebirth()
    {
        if (Lock)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                Lock = false;
            }
        }
        transform.gameObject.SetActive(false);
        transform.position = StartPos;
        transform.gameObject.SetActive(true);
    }
    public void Destroy()
    {
        if (Lock)
        {
            Destroy(gameObject);
        }
        else
        {
            StartPos = transform.position;
        }
    }
    public void Run()
    {
        EnemyStep.Post(gameObject);
    }
}
