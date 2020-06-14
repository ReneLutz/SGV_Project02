using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Original Destination
    // x -4.31, y 10.76, z -76.53

    [SerializeField] private int _life;

    [SerializeField] private Transform _target;

    private NavMeshAgent _agent;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _agent.destination = _target.position;
    }

    private void Update() 
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude / _agent.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
