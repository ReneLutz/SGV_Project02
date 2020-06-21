using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Damagable _damagable;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        _image.fillAmount = _damagable.HealthRatio;
    }
}
