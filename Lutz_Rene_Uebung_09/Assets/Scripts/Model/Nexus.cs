using UnityEngine;

public class Nexus : MonoBehaviour
{
    [SerializeField] int _playerLife = 10;
    public int PlayerLife => this._playerLife;
    private int _maxLife;

    public delegate void OnLifeLoss(int maxlife, int currentLife);
    public OnLifeLoss _onLifeLoss;

    private void Start()
    {
        this._maxLife = this._playerLife;
    }

    private void OnTriggerEnter(Collider other)
    {
        DamagableEnemy enemy = other.gameObject.GetComponent<DamagableEnemy>();
        if (enemy)
        {
            enemy.Die(false);
            this._playerLife--;

            if (_playerLife <= 0) 
                SceneController.SceneControl.SwitchToEndScreen();

            if (this._onLifeLoss != null)
                this._onLifeLoss.Invoke(this._maxLife, this._playerLife);
        }
    }
}
