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
        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyDeath -= Enemy_OnEnemyDeath;
    }

    private void Enemy_OnEnemyDeath()
    {
        enemiesAlive--;
        
        if(enemiesAlive <= 0)
        {
            SpawnWave();
        }
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

        Debug.Log("Starting Wave " + currWave);

        currWave++;
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        if(!mainCamera)
        {
            mainCamera = Camera.main;
        }

        Vector2 spawnPos;
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        int edge = Random.Range(0, 4);
        Vector2 camPos = mainCamera.transform.position;

        switch (edge)
        {
            case 0: // Top
                spawnPos = new Vector2(Random.Range(camPos.x - camWidth, camPos.x + camWidth), camPos.y + camHeight + screenMargin);
                break;
            case 1: // Bottom
                spawnPos = new Vector2(Random.Range(camPos.x - camWidth, camPos.x + camWidth), camPos.y - camHeight - screenMargin);
                break;
            case 2: // Left
                spawnPos = new Vector2(camPos.x - camWidth - screenMargin, Random.Range(camPos.y - camHeight, camPos.y + camHeight));
                break;
            case 3: // Right
                spawnPos = new Vector2(camPos.x + camWidth + screenMargin, Random.Range(camPos.y - camHeight, camPos.y + camHeight));
                break;
            default:
                spawnPos = Vector2.zero;
                break;
        }

        return spawnPos;
    }
}
