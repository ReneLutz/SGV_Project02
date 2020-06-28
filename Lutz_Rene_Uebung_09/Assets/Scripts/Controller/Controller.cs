using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    public List<Skill> Skills;

    [SerializeField] private Transform _projectileSpawn;
    [SerializeField] private ProjectilePool _projectilePool;

    private float _range;

    private Skill _currentSkill;
    private AudioSource _audio;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Damagable _target;
    private Transform _transform;

    public Controller Init(ProjectilePool pool)
    {
        _projectilePool = pool;
        _projectilePool._objectPrefab = _currentSkill.ProjectilePrefab;
        return this;
    }

    private void Awake()
    {
        _transform = transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();

        SetSkill(0);
    }

    private void Update()
    {
        _animator.SetFloat(Constants.ANIMATION_PARAM_SPEED, _agent.velocity.magnitude / _agent.speed);

        if (_target != null)
        {
            _animator.SetBool(Constants.ANIMATION_PARAM_INRANGE, Vector3.Distance(this._target.transform.position, this._transform.position) <= this._range);
        }

        if (_animator.GetBool(Constants.ANIMATION_PARAM_ATTACK))
        {
            if (_target)
            {
                if (_agent.enabled) _agent.destination = _target.transform.position;
            }
            else
            {
                _animator.SetBool(Constants.ANIMATION_PARAM_ATTACK, false);
            }
        }
    }

    private void Shoot()
    {
        if(!_target || !_target.Attackable)
        {
            _animator.SetBool(Constants.ANIMATION_PARAM_ATTACK, false);
            return;
        }

        Projectile projectile = _projectilePool.GetProjectile();
        projectile.Init(_projectileSpawn.position, _target, _currentSkill.Damage, _currentSkill.ProjectileSpeed);

        _audio.Play();
    }

    public void Attack(Damagable target)
    {
        _target = target;
        _animator.SetBool(Constants.ANIMATION_PARAM_ATTACK, true);
    }

    public void Move(Vector3 destination)
    {
        if (!_agent.enabled) return;

        _target = null;
        _agent.destination = destination;

        _animator.SetBool(Constants.ANIMATION_PARAM_ATTACK, false);
    }

    public void SetSkill(int index)
    {
        if (index >= Skills.Count) return;
        _currentSkill = Skills[index];
        _range = _currentSkill.Range;
        _audio.clip = _currentSkill.Soundeffect;
    }

    //Invoked from Animator
    public void StopAgent()
    {
        if (!_agent.enabled) return;

        _agent.isStopped = true;
    }

    public void StartAgent()
    {
        if (!_agent.enabled) return;

        _agent.isStopped = false;
    }
}
