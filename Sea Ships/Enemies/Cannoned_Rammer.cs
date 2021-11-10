using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannoned_Rammer : Rammer
{
    public Transform[] CannonHolders;
    public GameObject CannonBall;
    public override void engage()
    {
        base.engage();
        foreach (Transform Holder in CannonHolders)
            Holder.rotation = Quaternion.LookRotation(Player.position - transform.position);
            if (Time.time > attackTime)
            {
            foreach (Transform Holder in CannonHolders)
                Instantiate(CannonBall, Holder.position, Holder.rotation);
                attackTime = Time.time + RecoilTime;
            }
        }
    }
