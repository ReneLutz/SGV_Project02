using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Damagable _damagable;
    [SerializeField] private Image _image;

    void Update()
    {
        _image.fillAmount = _damagable.HealthRatio;
        transform.LookAt(Camera.main.transform);
    }
}
