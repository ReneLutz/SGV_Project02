using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image _healthbar;

    private float _currentHealth = 1;
    private float _maxHealth = 5;

    public virtual void Init(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;

        gameObject.SetActive(true);
    }

    void Update()
    {
        _healthbar.fillAmount = (_currentHealth / _maxHealth);
    }

    public void SetHealth(float amount)
    {
        _currentHealth = amount;
    }

    public void Dispose()
    {
        gameObject.SetActive(false);
    }
}
