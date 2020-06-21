using UnityEngine;

public class Nexus : MonoBehaviour
{
    [SerializeField] int _playerLife = 10;
    public int PlayerLife => this._playerLife;
    private int _maxLive;

    public delegate void OnLifeLoss(int maxlife, int currentLife);
    public OnLifeLoss _onLifeLoss;

    void Start()
    {
        this._maxLive = this._playerLife;
    }

    private void OnCollisionEnter(Collision other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            // ToDo: Play death explosion and add enemy back to pool
            Destroy(enemy.gameObject);
            this._playerLife--;

            if (this._onLifeLoss != null)
                this._onLifeLoss.Invoke(this._maxLive, this._playerLife);
        }
    }
}
