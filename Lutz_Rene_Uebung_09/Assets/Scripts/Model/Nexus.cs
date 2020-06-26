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
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            // ToDo: Play death explosion and add enemy back to pool
            Destroy(enemy.gameObject);
            this._playerLife--;

            if (this._onLifeLoss != null)
                this._onLifeLoss.Invoke(this._maxLife, this._playerLife);
        }
    }
}
