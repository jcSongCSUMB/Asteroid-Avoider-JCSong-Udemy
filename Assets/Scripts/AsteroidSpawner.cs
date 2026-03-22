using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float spawnRate;
    [SerializeField] private Vector2 forceRange;

    private float timer;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnAsteroid();
            timer = spawnRate;
        }
    }

    private void SpawnAsteroid()
    {
        int side = Random.Range(0, 4);
        Vector2 spawnPoint = Vector2.zero;
        Vector2 spawnDirection = Vector2.zero;

        switch (side)
        {
            case 0: // left side of the screen
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                spawnDirection = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            
            case 1: // right side of the screen
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                spawnDirection = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            
            case 2: // bottom side of the screen
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                spawnDirection = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            
            case 3: // top side of the screen
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;
                spawnDirection = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }
        
        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0;
        
        GameObject selectedAsteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        
        GameObject asteroidInstance = Instantiate(
            selectedAsteroid, 
            worldSpawnPoint, 
            Quaternion.Euler(0f, 0f,  Random.Range(0f, 360f)));
        Rigidbody asteroidRigidbody = asteroidInstance.GetComponent<Rigidbody>();

        asteroidRigidbody.linearVelocity= spawnDirection.normalized * Random.Range(forceRange.x, forceRange.y);

    }
}
