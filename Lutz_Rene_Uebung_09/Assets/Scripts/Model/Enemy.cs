using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float _attentionRange;

    [SerializeField] private Controller _controller;
    
    private Nexus _nexus;
    private Damagable _player;

    public Enemy Init(Nexus nexus, Damagable player, ProjectilePool pool)
    {
        _nexus = nexus;
        _player = player;

        _controller.Init(pool);
        return this;
    }

    private void Update()
    {
        // Distance between enemy and player
        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance <= _attentionRange && _player.Attackable)
        {
            _controller.Attack(_player);
        }
        else
        {
            _controller.Move(_nexus.transform.position);
        }
    }
}
