internal static class WaveTracker
{
    internal static int CurrentWaveCount;
    internal static int MaxWaveCount;

    internal static int CurrentEnemyCount;
    internal static int MaxEnemyCount;

    internal static void SetupWaveStats(int currentWave, int maxWave, int currentEnemyCount, int maxEnemyCount)
    {
        CurrentWaveCount = currentWave;
        MaxWaveCount = maxWave;
        CurrentEnemyCount = currentEnemyCount;
        MaxEnemyCount = maxEnemyCount;
    }
}
