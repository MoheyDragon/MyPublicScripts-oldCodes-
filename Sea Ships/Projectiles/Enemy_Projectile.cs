public class Enemy_Projectile : Projectiles
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        IsProjectileReal = true;
        TargetType = "Player";
    }
}
