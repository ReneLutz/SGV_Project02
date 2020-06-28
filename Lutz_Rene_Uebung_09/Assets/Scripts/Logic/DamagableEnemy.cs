using UnityEngine;

public class DamagableEnemy : Damagable
{
    public delegate void OnEnemyDeath(DamagableEnemy enemy);
    public OnEnemyDeath _onEnemyDeath;

    public override void Die()
    {
        Attackable = false;
        Instantiate(_explosion.gameObject, transform.position, Quaternion.identity);

        if (_onEnemyDeath != null)
            _onEnemyDeath.Invoke(this);

        Destroy(gameObject);
    }
}
