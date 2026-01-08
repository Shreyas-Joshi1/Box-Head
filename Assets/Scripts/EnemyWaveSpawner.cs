using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int baseEnemies = 3;
    [SerializeField] private float screenMargin = 1.5f;
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private LayerMask blockingLayers;
    [SerializeField] private Transform player;
    [SerializeField] private WaveNumberUI waveNumberUI;

    private int currWave = 1;
    private int enemiesAlive = 0;
    private Camera mainCamera;
    private float minDisFromPlayer;

    private void Awake()
    {
        mainCamera = Camera.main;
        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;

        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        minDisFromPlayer = Mathf.Max(camHeight, camWidth); //To not spawn enemies too close to player
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

        waveNumberUI.SetWaveNumber(currWave);
        currWave++;
    }

    // Spawns an enemy at a valid position -> not too close to player and not colliding with obstacles
    private void SpawnEnemy()
    {
        Vector2 spawnPos = spawnArea.bounds.center; //Default spawn position

        for (int i = 0; i < 20; i++) //Try max 20 times to find a valid spawn position
        {
            spawnPos = ClampToPlayArea(GetRandomSpawnPosition());

            if (IsSpawnValid(spawnPos)) break;
        }
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    // Check if spawn position is valid
    private bool IsSpawnValid(Vector2 spawnPos)
    {
        if(!IsFarFromPlayer(spawnPos)) return false;

        if(Physics2D.OverlapCircle(spawnPos, 0.4f, blockingLayers) != null) return false;

        return true;
    }

    private bool IsFarFromPlayer(Vector2 spawnPos)
    {
        return Vector2.Distance(spawnPos, player.position) >= minDisFromPlayer;
    }

    // Clamp spawn position to within the spawn area bounds
    private Vector2 ClampToPlayArea(Vector2 spawnPos)
    {
        Bounds b = spawnArea.bounds;

        float clampedX = Mathf.Clamp(spawnPos.x, b.min.x, b.max.x); //Clamp X within bounds
        float clampedY = Mathf.Clamp(spawnPos.y, b.min.y, b.max.y); //Clamp Y within bounds

        return new Vector2(clampedX, clampedY);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        if(!mainCamera)
        {
            mainCamera = Camera.main;
        }

        Vector2 spawnPos;
        float camHeight = mainCamera.orthographicSize; // Half height
        float camWidth = camHeight * mainCamera.aspect; // Half width

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
