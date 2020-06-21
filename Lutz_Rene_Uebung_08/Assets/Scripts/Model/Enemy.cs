using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;

    [SerializeField] private Transform _target;

    [SerializeField] private PlayerController _player;
    [SerializeField] private Healthbar _healthbar;

    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private SkinnedMeshRenderer _renderer;

    [SerializeField] private float _damage;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Rigidbody _body;
    private Transform _transform;
    private Camera _camera;
    
    public virtual void Init(Vector3 position, float life, float damage)
    {
        _transform.position = position;
        _health = life;
        _damage = damage;

        _agent.isStopped = false;

        _healthbar.Init(_health);
    }

    private void Awake()
    {
        _transform = transform;
        _camera = Camera.main;

        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();

        _healthbar.Init(_health);
    }

    private void Start()
    {
        _agent.destination = _target.position;
    }

    private void Update() 
    {
        _animator.SetFloat(Constants.ANIMATION_PARAM_SPEED, _agent.velocity.magnitude / _agent.speed);
        _healthbar.transform.LookAt(_camera.transform);
    }

    private void Explode()
    {
        _agent.isStopped = true;
        _body.detectCollisions = false;
        _renderer.enabled = false;
        _healthbar.Dispose();

        _explosion.Play();

        StartCoroutine(WaitOnParticleSystemEnd());
    }

    private IEnumerator WaitOnParticleSystemEnd()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        while(_explosion.isPlaying)
        {
            yield return wait;
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.TAG_PROJECTILE)
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            
            _health -= projectile.Damage;
            _healthbar.SetHealth(_health);

            if (_health <= 0)
            {
                _player.StopAttack();
                Explode();
            }
        }

        if (other.tag == Constants.TAG_BASE)
        {
            _player.LoseHealth(_damage);
            Explode();
        }
    }
}
