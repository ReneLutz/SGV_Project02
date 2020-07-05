public class ProjectilePool : ObjectPool<Projectile>
{
    public Projectile GetProjectile()
    {
        return GetObject();
    }
}
