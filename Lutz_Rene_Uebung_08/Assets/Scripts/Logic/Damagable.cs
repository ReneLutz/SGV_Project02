using UnityEngine;

public abstract class Damagable : MonoBehaviour
{
    [SerializeField] protected Explosion _explosion;

    [SerializeField] private int _maxHealth = 5;
    private int _currentHealth;
    
    public float HealthRatio => (float)this._currentHealth / (float)this._maxHealth;

    void Start()
    {
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
