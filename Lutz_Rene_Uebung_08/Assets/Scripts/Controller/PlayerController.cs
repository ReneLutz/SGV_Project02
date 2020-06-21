using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _projectileSpawn;
    [SerializeField] private Healthbar _healthbar;
    [SerializeField] private ProjectilePool _projectilePool;

    [SerializeField] private float _projectileFrequency;
    [SerializeField] private float _range;

    public float Health;

    private float _projectileCooldown;
    private bool _hasTarget;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Transform _target;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _healthbar.Init(Health);
        
        // If not set on frequency here, the player would have to wait the cooldown to attack the first time
        _projectileCooldown = _projectileFrequency;
        _hasTarget = false;
    }

    private void Update()
    {
        _animator.SetFloat(Constants.ANIMATION_PARAM_SPEED, _agent.velocity.magnitude / _agent.speed);

        _projectileCooldown += Time.deltaTime;

        if (_hasTarget)
        {
            CheckRange();
        }
    }

    private void CheckRange()
    {
        Vector3 distance = _target.position - _transform.position;
        
        // If player is in range
        if (distance.magnitude < _range)
        {
            Shoot();
        }
        else
        {
            Move(_target.position, _hasTarget);
        }
    }

    private void Shoot()
    {
        // Check if attack is on cooldown. If yes, abort.
        if (_projectileCooldown < _projectileFrequency) return;
        
        _agent.isStopped = true;
        _animator.SetBool(Constants.ANIMATION_PARAM_ATTACK, true);

        Projectile projectile = _projectilePool.GetProjectile();
        projectile.Init(_projectileSpawn.position, _target); 
        _projectileCooldown = 0;
    }

    public void Attack(Transform target)
    {
        _target = target;
        _hasTarget = true;
    }

    public void StopAttack()
    {   
        _animator.SetBool(Constants.ANIMATION_PARAM_ATTACK, false);
        _hasTarget = false;
    }

    public void Move(Vector3 position, bool hasTarget = false)
    {
        _agent.isStopped = false;
        _agent.destination = position;

        _animator.SetBool(Constants.ANIMATION_PARAM_ATTACK, false);
        _hasTarget = hasTarget;
    }

    public void LoseHealth(float amount)
    {
        Health -= amount;
        _healthbar.SetHealth(Health);   
    }
}
