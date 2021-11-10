using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rammer : EnemyShip
{
    bool NewLock;
    public override void engage()
    {
        if(!NewLock&&!Freezelook)
        {
            NewLock = true;
            Invoke("Wait", RecoilTime/3);
            Invoke("Attack", RecoilTime*2/3);
        }
    }
    void Wait()
    {
        Vector3 MoveDirection = new Vector3(0, 0, -10);
        MoveDirection = transform.TransformDirection(MoveDirection);
        rb.AddForce(MoveDirection, ForceMode.VelocityChange);
        agent.isStopped = true;
    }
    void Attack()
    {
        if (!Freezelook)
            agent.isStopped = false;
        NewLock = false;

    }

}
