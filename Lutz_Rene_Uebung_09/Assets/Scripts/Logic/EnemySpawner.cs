using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _waves;

    [SerializeField] private Nexus _nexus;
    [SerializeField] private Damagable _player;

    [SerializeField] private ProjectilePool _projectilePool;

    [SerializeField] private int _waveSize;     // Number of enemies per wave
    [SerializeField] private int _waveDelay;    // Time between two waves

    private float _nextWaveTimer;       // Time until next wave
    private int _nextWaveIndex;         // Next wave index
    private int _currentWaveIndex;      // Current wave index

    private float _enemyDelay;          // Time between two enemies within a single wave
    private float _enemyTimer;
    private int _currentEnemyCount;     // Number of enemies in current wave

    private bool _spawnCurrentWave = false;

    // Start is called before the first frame update
    void Start()
    {
        _nextWaveIndex = 0;
        _nextWaveTimer = 5f;
        _enemyDelay = 0.5f;
        _enemyTimer = _enemyDelay;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_spawnCurrentWave) SpawnCurrentWave();

        // Check timer for new wave
        _nextWaveTimer -= Time.deltaTime;
        if (_nextWaveTimer > 0) return;
        _nextWaveTimer = _waveDelay;

        if (_nextWaveIndex < _waves.Count)
        {
            _currentWaveIndex = _nextWaveIndex;
            _nextWaveIndex++;
            _spawnCurrentWave = true;
            _currentEnemyCount = 0;
        }
    }

    private void SpawnCurrentWave()
    {
        // Check timer for next enemy
        _enemyTimer -= Time.deltaTime;
        if (_enemyTimer > 0) return;
        _enemyTimer = _enemyDelay;

        // Spawn new enemy and instantiate nexus as target
        Instantiate(_waves[_currentWaveIndex], transform).Init(_nexus, _player, _projectilePool);
        _currentEnemyCount++;

        // Check if spawn more enemies
        _spawnCurrentWave = _currentEnemyCount < _waveSize;
    }
}
