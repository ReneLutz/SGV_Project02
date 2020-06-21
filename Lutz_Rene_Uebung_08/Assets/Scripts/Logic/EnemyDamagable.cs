using UnityEngine;

public class EnemyDamagable : Damagable
{
    public override void Die()
    {
        Instantiate(this._explosion.gameObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
