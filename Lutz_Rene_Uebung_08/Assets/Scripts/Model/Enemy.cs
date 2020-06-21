using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Controller _controller;
    [SerializeField] GameObject _target;

    public Enemy Init(GameObject target)
    {
        _target = target;
        _controller.Move(_target.transform.position);
        return this;
    }

    private void Start()
    {
        Init(_target);
    }
}
