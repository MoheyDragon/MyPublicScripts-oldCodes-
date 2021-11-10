using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner_Sailor : EnemyShip
{
    public GameObject Gunner;
    float attackTime;
    public GameObject GunneryBullet;
    public Transform Holder;
    public override void engage()
    {
        base.engage();
        Gunner.transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
        
        if (Time.time > attackTime)
        {
            Instantiate(GunneryBullet, Holder.position, Holder.rotation);
            Debug.Log(this.name + " is attacking you!");
            attackTime = Time.time + RecoilTime;
        }
        
    }
}
