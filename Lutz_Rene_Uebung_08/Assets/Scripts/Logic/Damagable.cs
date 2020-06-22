using UnityEngine;

public abstract class Damagable : MonoBehaviour
{
    [SerializeField] protected Explosion _explosion;

    [SerializeField] protected int _maxHealth = 5;

    public bool Attackable;

    protected int _currentHealth;
    
    public float HealthRatio => (float)this._currentHealth / (float)this._maxHealth;

    void Start()
    {
        Attackable = true;
        this._currentHealth = this._maxHealth;
    }

    public void Hit(int damage = 1)
    {
        this._currentHealth -= damage;
        if (this._currentHealth <= 0)
            this.Die();
    }

    public abstract void Die();
}
