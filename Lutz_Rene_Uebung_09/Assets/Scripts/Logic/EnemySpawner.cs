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

    private List<Damagable> _activeEnemies = new List<Damagable>();

    private float _nextWaveTimer;       // Time until next wave
    private int _nextWaveIndex;         // Next wave index
    private int _currentWaveIndex;      // Current wave index

    private float _enemyDelay;          // Time between two enemies within a single wave
    private float _enemyTimer;

    private bool _spawnCurrentWave = false;

    // Start is called before the first frame update
    private void Start()
    {
        _nextWaveIndex = 0;
        _nextWaveTimer = 5f;
        _enemyDelay = 0.5f;
        _enemyTimer = _enemyDelay;

        WaveTracker.CurrentWaveCount = 0;
        WaveTracker.MaxWaveCount = _waves.Count;
        WaveTracker.MaxEnemyCount = _waveSize;
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

            WaveTracker.CurrentWaveCount = _nextWaveIndex;
        }
    }

    private void SpawnCurrentWave()
    {
        // Check timer for next enemy
        _enemyTimer -= Time.deltaTime;
        if (_enemyTimer > 0) return;
        _enemyTimer = _enemyDelay;

        // Spawn new enemy and instantiate nexus as target
        Enemy enemy = Instantiate(_waves[_currentWaveIndex], transform).Init(_nexus, _player, _projectilePool);

        // Register callback for OnEnemyDeath event
        DamagableEnemy damagable = enemy.gameObject.GetComponent<DamagableEnemy>();
        damagable._onEnemyDeath += RemoveActiveEnemy;

        _activeEnemies.Add(damagable);

        WaveTracker.CurrentEnemyCount = _activeEnemies.Count;

        // Check if spawn more enemies
        _spawnCurrentWave = _activeEnemies.Count < _waveSize;
    }

    private void RemoveActiveEnemy(DamagableEnemy enemy)
    {
        _activeEnemies.Remove(enemy);

        WaveTracker.CurrentEnemyCount = _activeEnemies.Count;

        if (_nextWaveIndex < _waves.Count) return;

        // At this point the last wave has been reached. Check if all enemies are dead
        if (_activeEnemies.Count <= 0) SceneController.SceneControl.SwitchToEndScreen();
    }
}
