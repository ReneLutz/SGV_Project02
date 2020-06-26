using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Damagable _damagable;
    [SerializeField] private Image _imageFG;
    [SerializeField] private Image _imageBG;

    void Update()
    {
        bool draw = _damagable.HealthRatio > 0;

        _imageFG.enabled = draw;
        _imageBG.enabled = draw;

        if (draw)
        {
            _imageFG.fillAmount = _damagable.HealthRatio;
            transform.LookAt(Camera.main.transform);
        }
    }
}
