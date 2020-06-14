using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private readonly string SPEED = "Speed";
    private readonly string ATTACK = "Attack";

    private NavMeshAgent _agent;

    private Animator _animator;

    private bool _enableMove = false;
    private bool _attackTriggered = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(SPEED, _agent.velocity.magnitude / _agent.speed);

        Attack();
    }

    private void Attack()
    {
        // Abort if already attacking, but allow to trigger next attack first
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            _attackTriggered = false;
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            // If the character was moving, stop it
            if (!_agent.isStopped)
            {
                _enableMove = true;
                _agent.isStopped = true;
            }

            // Trigger attack only if not triggered since last attack animation
            if (!_attackTriggered)
            {
                _attackTriggered = true;
                _animator.SetTrigger(ATTACK);
            }
        }
        else
        {
            // Here neither attack animation is playing nor attack button is pressed.
            // Allow attacking and set player moving if player moved before
            _attackTriggered = false;

            if (_enableMove)
            {
                _enableMove = false;
                _agent.isStopped = false;
            }
        }
    }

    public void Move(Vector3 position)
    {
        _agent.destination = position;
    }
}
