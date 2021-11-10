public class Player_Projectile : Projectiles
{

    public override void Start()
    {
        base.Start();
        IsProjectileReal = true;
        TargetType = "Enemy";

    }
}
