using UnityEngine;

public class DamagableEnemy : Damagable
{
    [SerializeField] private int experience;

    public delegate void OnEnemyDeath(DamagableEnemy enemy);
    public delegate void OnExperienceGain(int experience);
   
    public OnEnemyDeath _onEnemyDeath;
    public OnExperienceGain _onExperienceGain;

    public override void Die(bool rewardExperience = true)
    {
        Attackable = false;
        Instantiate(_explosion.gameObject, transform.position, Quaternion.identity);

        if (_onExperienceGain != null && rewardExperience)
            _onExperienceGain.Invoke(experience);

        if (_onEnemyDeath != null)
            _onEnemyDeath.Invoke(this);

        Destroy(gameObject);
    }
}
