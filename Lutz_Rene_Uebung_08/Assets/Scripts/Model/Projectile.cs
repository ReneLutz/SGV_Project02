using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 _bodyOffset;

    private float _projectileSpeed;

    private int _damage;

    private Rigidbody _body;
    private Damagable _target;
    private Transform _transform;

    public Projectile Init(Vector3 position, Damagable target, int damage, int projectileSpeed)
    {
        _target = target;
        _transform.position = position;

        _damage = damage;
        _projectileSpeed = projectileSpeed;

        return this;
    }

    private void Awake()
    {
        _transform = transform;
        _body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(_target == null || !_target.Attackable)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 direction = (_target.transform.position - _body.position).normalized;
        _body.MovePosition(_body.position + direction * Time.fixedDeltaTime * _projectileSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If collider is not the target, abort
        if (other.gameObject != _target.gameObject) return;

        Damagable enemy = other.GetComponent<Damagable>();

        if (enemy != null)
        {
            enemy.Hit(_damage);
            gameObject.SetActive(false);
        }
    }
}
