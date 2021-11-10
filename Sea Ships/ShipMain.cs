using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShipMain : MonoBehaviour
{
    
    public float RotateSpeed = 1;
    public float MoveSpeed ;
    public int RammingFrontPower, RammingSidePower;
    protected Animator Anime;
    protected Rigidbody rb;
    public int Health = 500; //health Ui Should be put in this script for all ships 
    protected int MaxHealth;
    public ParticleSystem WavesEffect;
    protected ParticleSystem WavesParticle;
    public Transform WavesHolder;
    

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();

        try
        {
            Anime = this.GetComponent<Animator>();
        }
        catch (System.Exception)
        {
            Anime = null;
            throw;
        }
        WaveInstantiate();

    }
    public virtual void Awake()
    {
        MaxHealth = Health;

    }

    public virtual void Update()
    {
        
    }
    public void Damage(int damage)
    {
        Health -= damage;
        Health = Mathf.Max(0, Health);
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    virtual public void OnCollisionEnter(Collision col)
    {

    }
    public virtual void  WaveInstantiate()
    {
        WavesParticle = Instantiate(WavesEffect, WavesHolder.position, WavesHolder.rotation, WavesHolder).GetComponent<ParticleSystem>();
        WavesParticle.startSize = transform.localScale.y;
    }
}
