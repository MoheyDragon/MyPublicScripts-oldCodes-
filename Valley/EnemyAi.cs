using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    Animator anime;
    NavMeshAgent aiAgent;
    Vector3 velocity;
    public int MoveSpeed = 5;float gravity = -20f;
    Transform GroundCheck;float groundDistance = .4f;LayerMask groundMask;bool IsGrounded,attackLock, fleeLock,secondFleeLock;
    //Ai
    float Distance;float Damping = 6;Transform Target;
    public float RecoilTime, AttackRange, SpotingDistance; float enemyDistance, attackTime;
    public int Health, damage;
    public float RotateSpeed = 50;
    public AK.Wwise.Event scream,footstep;
    bool screamLock = false;

    //public AK.Wwise.Event footstep, jump, jumpHigh,land, CheckPointEvent,Respwan,Lose,JumpChange;
    FleeManager flee;
    void Start()
    {
        fleeLock = secondFleeLock = false;
        Target =GameObject.Find("Hero").transform;
        anime = GetComponent<Animator>();
        aiAgent = GetComponent<NavMeshAgent>();

        aiAgent.speed = MoveSpeed;
        aiAgent.stoppingDistance = AttackRange;
        foreach (Transform child in transform)
        {
            if (GroundCheck == null)
            {
                if (child.tag == "GroundCheck")
                GroundCheck = child;
            }
        }
        groundMask= LayerMask.GetMask("Ground");
        anime.SetFloat("Animation Speed", 1);

        flee = transform.parent.GetComponent<FleeManager>();
    }
    void Update()
    {
        
        IsGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        if (IsGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        if (Health>25)
        {
            Distance = Vector3.Distance(new Vector3(Target.position.x, transform.position.y, Target.position.z), transform.position);
            if (Distance > SpotingDistance)
            {
                anime.SetBool("Moving", false);
                anime.SetFloat("Velocity",0);
                
            }
            if (Distance <= SpotingDistance && !attackLock)
            {
                Quaternion rotation = Quaternion.LookRotation(new Vector3(Target.position.x, transform.position.y, Target.position.z) - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
                anime.SetBool("Moving", true);
                anime.SetFloat("Velocity",1);
                if (!screamLock)
                {
                    scream.Post(gameObject);
                    screamLock = true;
                }
                if (!aiAgent.isStopped)
                    aiAgent.SetDestination(Target.position);
                if (Distance > AttackRange)
                {
                    aiAgent.isStopped = false;
                    anime.SetBool("Moving", true);
                    anime.SetFloat("Velocity", 1);
                }
            }
            if (Distance < AttackRange && !attackLock)
            {
                if (Time.time > attackTime)
                {
                    anime.SetInteger("Trigger Number", 2);
                    anime.SetTrigger("Trigger");
                    attackLock = true;
                    attackTime = Time.time + RecoilTime;
                }
                velocity.y += gravity * Time.deltaTime;
                aiAgent.isStopped = true;
                anime.SetBool("Moving", false);
                anime.SetFloat("Velocity", 0);
            }
            
        }
        else
        {
            Distance = Vector3.Distance(aiAgent.destination, transform.position);
            if (!fleeLock&&!secondFleeLock)
            {
                aiAgent.isStopped = false;
                anime.SetBool("Moving", true);
                anime.SetFloat("Velocity", 1);
                aiAgent.SetDestination(flee.FleeTargets[Random.Range(1, 4)]);
                fleeLock = true;
            }
            if (Distance<3&&!secondFleeLock&&fleeLock)
            {
                aiAgent.SetDestination(flee.FleeTargets[Random.Range(1, 4)]);
                secondFleeLock = true;
                fleeLock = false;
            }
            else if (Distance<3&&secondFleeLock)
            {
                fleeLock = false;
                secondFleeLock = false;
            }
            Quaternion rotation = Quaternion.LookRotation(aiAgent.destination);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);

        }

    }
    public void Attack()
    {
        if (Physics.CheckSphere(transform.position, 5))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5);
            foreach (Collider col in colliders)
                if (col.CompareTag("Player"))
                        col.GetComponent<CombatSystem>().Damage(damage);
        }
        anime.SetFloat("Velocity", 0);
        attackLock = false;
    }
    public void Damage(int dam)
    {
        Health -= dam;
        if (Health<=0)
        {
            Destroy(gameObject);
        }
    }
    public void step()
    {
        footstep.Post(gameObject);
    }
}
