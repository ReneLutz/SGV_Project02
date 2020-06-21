using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagable : Damagable
{
    public override void Die()
    {
        Instantiate(this._explosion.gameObject, transform.position, Quaternion.identity);
        
        // Respawn player
    }
}
