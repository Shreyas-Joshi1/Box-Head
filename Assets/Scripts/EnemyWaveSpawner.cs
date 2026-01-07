using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int baseEnemies = 3;
    [SerializeField] private float screenMargin = 1.5f;

    private int currWave = 1;
    private int enemiesAlive = 0;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        SpawnWave();
    }

    private void SpawnWave()
    {
        int enemiesToSpawn = baseEnemies + currWave;
        enemiesAlive = enemiesToSpawn;

        for(int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }

        currWave++;
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPos;
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        int edge = Random.Range(0, 4);

        switch(edge)
        {
            case 0: // Top
                spawnPos = new Vector2(Random.Range(-camWidth, camWidth), camHeight + screenMargin);
                break;
            case 1: // Bottom
                spawnPos = new Vector2(Random.Range(-camWidth, camWidth), -camHeight - screenMargin);
                break;
            case 2: // Left
                spawnPos = new Vector2(-camWidth - screenMargin, Random.Range(-camHeight, camHeight));
                break;
            case 3: // Right
                spawnPos = new Vector2(camWidth + screenMargin, Random.Range(-camHeight, camHeight));
                break;
            default:
                spawnPos = Vector2.zero;
                break;
        }

        return spawnPos;
    }
}
