using UnityEngine;
using UnityEngine.AI;

public class DamagablePlayer : Damagable
{
    [SerializeField] private float _respawnDelay;

    [SerializeField] private Transform _nexusTransform;

    [SerializeField] private SkinnedMeshRenderer _renderer;
    
    private float _respawnTimer;
    private bool _respawn = false;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Rigidbody _body;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_respawn)
        {
            _respawnTimer -= Time.deltaTime;
            if (_respawnTimer > 0) return;

            Respawn();
            SetActive(true);
        }
    }

    private void SetActive(bool active)
    {
        _respawn = !active;
        Attackable = active;

        _agent.enabled = active;
        _animator.enabled = active;
        _body.detectCollisions = active;
        _renderer.enabled = active;
    }

    private void Respawn()
    {
        transform.position = _nexusTransform.position;
        _currentHealth = _maxHealth;
    }

    public override void Die(bool rewardExperience = true)
    {
        Instantiate(this._explosion.gameObject, transform.position, Quaternion.identity);

        _respawnTimer = _respawnDelay;
        SetActive(false);
    }
}
