using UnityEngine;

public class DamagableEnemy : Damagable
{
    public override void Die()
    {
        Instantiate(this._explosion.gameObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
