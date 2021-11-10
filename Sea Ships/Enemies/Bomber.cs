using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : EnemyShip
{
    public GameObject Explode;
    public override void engage()
    {
        base.engage();
        Instantiate(Explode,transform.position,transform.rotation);
        Destroy(gameObject);
    }
    public override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
        if (col.collider.tag == "Player")
        {
            engage();
        }

    }
}
