using UnityEngine;

public class BackFire : Player_Projectile
{
    public int BulletType;
    public int Time;

    public override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (BulletType == 2)
        {
            if (col.gameObject.tag == TargetType)
            {
                col.GetComponent<EnemyShip>().Freeze(Power, Time);
            }
        }
    }


}
