using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonOnBoat : EnemyShip
{
    public GameObject CannonBall;
    public Transform CannonBallHolder;
    public override void engage()
    {
        base.engage();
        Quaternion LookRot = Quaternion.LookRotation(Player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, LookRot, Time.deltaTime);
        if (Time.time > attackTime)
        {
            Instantiate(CannonBall, CannonBallHolder.position, CannonBallHolder.rotation);
            Debug.Log(this.name + " is attacking you!");
            attackTime = Time.time + RecoilTime;
        }

    }
}
