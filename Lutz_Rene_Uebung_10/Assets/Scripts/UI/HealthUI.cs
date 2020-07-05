using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Nexus _nexus;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _image;

    private void Start()
    {
        _nexus._onLifeLoss += Display;
        Display(_nexus.PlayerLife, _nexus.PlayerLife);
    }

    private void Display(int maxLive, int currentLive)
    {
        float ratio = (float)currentLive / (float)maxLive;

        _healthText.text = currentLive.ToString();
        _image.fillAmount = ratio;
    }
}
