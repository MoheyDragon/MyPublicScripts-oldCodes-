using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int health;
    public int maxHealth = 1;
    [SerializeField] private float speed= 0.001f;
    Animator anime;
    Vector3 forward;
    Rigidbody rb;
    int random;
    LevelManager level;
    bool IsAttacking;
    Vector3 up = new Vector3(0, 180);
    Vector3 down = new Vector3(0, 0);
    Vector3 rightRotation = new Vector3(0, 270);
    Vector3 leftRotation = new Vector3(0, 90);
    Vector3 right = new Vector3(0, 0,5);
    Vector3 left= new Vector3(0,0,5);
    private void Start()
    {
        IsAttacking = false;
        level = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        health = maxHealth;
        anime = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        anime.SetTrigger("Run");
    }
    void Update()
    {
        if (IsAttacking) return;
        transform.Translate(forward);
    }
    public void damage(int amount)
    {
        health -= amount;
        
        if (health <= 0)
        {
            GetComponent<Collider>().enabled = false;
            IsAttacking = true;
            random= Random.Range(0, 2);
            if (random==0)
                anime.SetTrigger("Hit0");
            else
                anime.SetTrigger("Hit1");
            LevelManager.DeathParticleIndex = instantiateParticle(LevelManager.deathPool, LevelManager.DeathParticleIndex);
            health = maxHealth;
        }
        else
        {
            anime.SetTrigger("damage");//not avalible yet
            LevelManager.hitParticleIndex = instantiateParticle(LevelManager.enemyHitPool, LevelManager.hitParticleIndex);
        }
    }
    public void changeForward(Vector3 newForward,int direction)
    {
        transform.position = newForward;
        GetComponent<Collider>().enabled = true;
        IsAttacking = false;
        if (direction == 0)
        {
            transform.eulerAngles = up;
            forward = newForward * speed;
        }
        else if (direction == 1)
        {
            transform.eulerAngles = rightRotation;
            forward = right * speed;
        }
        else if (direction == 2)
        {
            transform.eulerAngles = down;
            forward = -newForward * speed;
        }
        else
        {
            transform.eulerAngles = leftRotation ;
            forward = left* speed;
        }
            
    }
    int instantiateParticle(ParticleSystem[] pool,int index)
    {
        pool[index].gameObject.transform.position = transform.position;
        pool[index].gameObject.SetActive(true);
        pool[index].Play();
        index++;
        if (index == 25)
            index = 0;
        return index;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletSpawn"))
        {
            anime.SetTrigger("Attack");
            IsAttacking = true;
        }
    }
    public void HitPlayer()
    {
        level.PlayerHit();
        IsAttacking = false;
        gameObject.SetActive(false);
    }
    public void EnemyDeath()
    {
        anime.SetTrigger("Run");
        gameObject.SetActive(false);
    }
}
