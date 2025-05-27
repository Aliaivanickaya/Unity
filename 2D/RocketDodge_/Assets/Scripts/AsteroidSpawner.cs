using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 1.5f;
    public float xRange = 8f;
    private float difficultyTimer = 0f;
    public float difficultyIncreaseInterval = 30f; 
    public float minSpawnInterval = 0.3f; 


    void Start()
    {
        InvokeRepeating("SpawnAsteroid", 1f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        float spawnY = Camera.main.orthographicSize + 2f;

        Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), spawnY, 0f);
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        float scale = Random.Range(2.3f, 4.0f);
        asteroid.transform.localScale = new Vector3(scale, scale, 1f);

        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, -2f);
        }
    }


    void Update()
    {
        difficultyTimer += Time.deltaTime;

        if (difficultyTimer >= difficultyIncreaseInterval)
        {
            difficultyTimer = 0f;

            if (spawnInterval > minSpawnInterval)
            {
                spawnInterval -= 0.2f;
                CancelInvoke("SpawnAsteroid");
                InvokeRepeating("SpawnAsteroid", 0f, spawnInterval);
            }
        }
    }

}


