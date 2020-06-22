using UnityEngine;

public class DamagablePlayer : Damagable
{
    public override void Die()
    {
        Instantiate(this._explosion.gameObject, transform.position, Quaternion.identity);
        
        // Respawn player
    }
}
