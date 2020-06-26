using UnityEngine;

public class DamagableEnemy : Damagable
{
    public override void Die()
    {
        Attackable = false;
        Instantiate(this._explosion.gameObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
