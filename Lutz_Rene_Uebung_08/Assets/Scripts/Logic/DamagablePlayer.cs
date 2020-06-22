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
    private Rigidbody _body;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_respawn)
        {
            _respawnTimer -= Time.deltaTime;
            if (_respawnTimer > 0) return;

            SetActive(true);
            transform.position = _nexusTransform.position;
        }
    }

    private void SetActive(bool active)
    {
        _respawn = !active;
        
        _agent.isStopped = !active;
        _body.detectCollisions = active;
        _renderer.enabled = active;
    }

    public override void Die()
    {
        Instantiate(this._explosion.gameObject, transform.position, Quaternion.identity);

        // Respawn player
        _respawnTimer = _respawnDelay;
        SetActive(false);
    }
}
