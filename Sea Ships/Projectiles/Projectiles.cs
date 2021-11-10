using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    protected Rigidbody rb;
    public int Power;
    public float Speed;
    public float LifeTime = 1;
    protected string TargetType;
    protected bool IsProjectileReal;
    virtual public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * Speed, ForceMode.Impulse);
        Destroy(gameObject, LifeTime);
    }

    virtual public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == TargetType)
        {
            col.GetComponent<ShipMain>().Damage(Power);
            if (IsProjectileReal)
            {
                Destroy(gameObject);
            }
            
        }
    }
    }

