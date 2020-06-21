using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private TextMeshProUGUI _healthText;

    void Update()
    {
        _healthText.text = string.Format("{0}", _player.Health);
    }
}
