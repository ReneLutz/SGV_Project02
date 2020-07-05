using TMPro;
using UnityEngine;

public class WaveTrackerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _waveText;
    [SerializeField] TextMeshProUGUI _enemyText;

    // Start is called before the first frame update
    void Start()
    {
        _waveText.text = string.Format(Constants.ENDSCREEN_WAVETEXT, WaveTracker.CurrentWaveCount, WaveTracker.MaxWaveCount);
        _enemyText.text = string.Format(Constants.ENDSCREEN_ENEMYTEXT, WaveTracker.CurrentEnemyCount, WaveTracker.MaxWaveCount);
    }
}
