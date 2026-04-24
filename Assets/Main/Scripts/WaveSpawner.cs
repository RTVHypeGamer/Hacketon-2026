using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;

        public float spawnInterval = 2f;
        public float waveDuration = 20f;

        public int maxEnemiesAlive = 10;
    }

    public Wave[] waves;

    public Transform[] spawnPoints;

    private int currentWaveIndex = 0;

    private float waveTimer = 0f;
    private float spawnTimer = 0f;

    private int enemiesAlive = 0;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (currentWaveIndex >= waves.Length)
            return;

        Wave wave = waves[currentWaveIndex];

        waveTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        // Spawn enemy
        if (spawnTimer >= wave.spawnInterval)
        {
            if (enemiesAlive < wave.maxEnemiesAlive)
            {
                SpawnEnemy();
            }

            spawnTimer = 0f;
        }

        // Next wave based on time
        if (waveTimer >= wave.waveDuration)
        {
            NextWave();
        }
    }

    void StartWave()
    {
        waveTimer = 0f;
        spawnTimer = 0f;

        Debug.Log("Wave " + (currentWaveIndex + 1));
    }

    void NextWave()
    {
        currentWaveIndex++;

        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("All waves finished");
            return;
        }

        StartWave();
    }

    void SpawnEnemy()
    {
        Wave wave = waves[currentWaveIndex];

        Transform spawnPoint =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy =
            Instantiate(
                wave.enemyPrefab,
                spawnPoint.position,
                Quaternion.identity
            );

        enemiesAlive++;

        // detect death automatically
        EnemyDeath death =
            enemy.GetComponent<EnemyDeath>();

        if (death != null)
        {
            death.spawner = this;
        }
    }

    public void EnemyDied()
    {
        enemiesAlive--;
    }
}
