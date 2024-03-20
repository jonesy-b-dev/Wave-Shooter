using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
// Serializable:
    [SerializeField] private GameObject enemy;
    [SerializeField] private int[] waveEnemyCount; 

// Public:
    public static GameManager instance;
    public GameObject pauseMenu;
    public float playerHealth = 100;
    public bool hasDied = false;
    public bool isPaused = false;
    public bool hasWon = false;
    public int enemiesLeft;
    public Vector2 xzEnemySpawnBounds = new Vector2(-10f, 10f);
    public float yCoordinate = 0f;

// Private
    private int currentWave;


    private void Awake()
    {
       if (instance != null && instance != this) Destroy(this);
       else instance = this;
    }

    public void EnemyDeath()
    {
        enemiesLeft -= 1;
        Debug.Log(enemiesLeft);

        if (enemiesLeft == 0)
        {
            SceneManager.LoadScene("Win Screen");
        }
    }
    private void Start()
    {
        currentWave = 0;
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        enemiesLeft = waveEnemyCount[currentWave];
        for (int i = 0; i < waveEnemyCount[currentWave]; i++)
        {
            // Generate random x and z coordinates within bounds
            float randomX = Random.Range(xzEnemySpawnBounds.x, xzEnemySpawnBounds.y);
            float randomZ = Random.Range(xzEnemySpawnBounds.x, xzEnemySpawnBounds.y);

            // Create a new position using the random coordinates and fixed y-coordinate
            Vector3 randomPosition = new Vector3(randomX, yCoordinate, randomZ);

            Instantiate(enemy, randomPosition, Quaternion.identity);
        }
    }
}
