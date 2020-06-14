using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _life;

    [SerializeField] private Transform _target;

    [SerializeField] private PlayerController _player;

    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private SkinnedMeshRenderer _renderer;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Rigidbody _body;
    private Transform _transform;

    public virtual void Init(Vector3 position, int life)
    {
        _transform.position = position;
        _life = life;

        _agent.isStopped = false;
    }

    private void Awake()
    {
        _transform = transform;

        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _agent.destination = _target.position;
    }

    private void Update() 
    {
        _animator.SetFloat(Constants.ANIMATION_PARAM_SPEED, _agent.velocity.magnitude / _agent.speed);
    }

    private void Explode()
    {
        _agent.isStopped = true;
        _body.detectCollisions = false;
        _renderer.enabled = false;

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
            
            _life -= projectile.Damage;
            
            if (_life <= 0)
            {
                Explode();
                _player.StopAttack();
            }
        }
    }
}
