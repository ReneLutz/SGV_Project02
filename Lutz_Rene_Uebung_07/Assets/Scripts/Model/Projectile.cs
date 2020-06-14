using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 _bodyOffset;
    [SerializeField] private float _projectileSpeed;

    [SerializeField] private float _lifetime;
    private float _projectileTTL;

    public int Damage;

    private Transform _target;

    private Transform _transform;

    public virtual void Init(Vector3 position, Transform target)
    {
        _transform.position = position;
        _target = target;

        _projectileTTL = _lifetime;
    }

    private void Awake()
    {
        _transform = transform;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = (_target.position + _bodyOffset)- _transform.position;

        _transform.Translate(direction.normalized * _projectileSpeed * Time.deltaTime);

        _projectileTTL -= Time.deltaTime;

        if (_projectileTTL <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants. TAG_ENEMY)
        {
            gameObject.SetActive(false);
        }
    }
}
