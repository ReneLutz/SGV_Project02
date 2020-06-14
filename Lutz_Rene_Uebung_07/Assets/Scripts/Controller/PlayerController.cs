using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _projectileSpawn;
    [SerializeField] private Healthbar _healthbar;
    [SerializeField] private ProjectilePool _projectilePool;

    [SerializeField] private float _projectileFrequency;
    
    public float Health;

    private float _projectileCooldown = 0;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Transform _target;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _healthbar.Init(Health);
    }

    private void Update()
    {
        _animator.SetFloat(Constants.ANIMATION_PARAM_SPEED, _agent.velocity.magnitude / _agent.speed);

        _projectileCooldown += Time.deltaTime;

        if(_projectileCooldown > _projectileFrequency)
        {
            _projectileCooldown = 0;
            Shoot();
        }

    }

    private void Shoot()
    {
        // If attack animation is set, spawn a projectile
        if (!_animator.GetBool(Constants.ANIMATION_PARAM_ATTACK)) return;

        Projectile projectile = _projectilePool.GetProjectile();
        projectile.Init(_projectileSpawn.position, _target);
    }

    public void Attack(Transform target)
    {
        _target = target;

        _agent.isStopped = true;
        _animator.SetBool(Constants.ANIMATION_PARAM_ATTACK, true);
    }

    public void StopAttack()
    {
        _animator.SetBool(Constants.ANIMATION_PARAM_ATTACK, false);
    }

    public void Move(Vector3 position)
    {
        _agent.isStopped = false;
        _agent.destination = position;

        _animator.SetBool(Constants.ANIMATION_PARAM_ATTACK, false);
    }

    public void LoseHealth(float amount)
    {
        Health -= amount;
        _healthbar.SetHealth(Health);   
    }
}
