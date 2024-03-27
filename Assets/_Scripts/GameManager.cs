using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
// Serializable:
    [SerializeField] private GameObject enemy;
    [SerializeField] private int[] waveEnemyCount;
    [SerializeField] private TextMeshProUGUI waveCountText;

// Public:
    public static GameManager instance;
    public float playerHealth = 100;
    public bool hasDied = false;
    public bool isPaused = false;
    public bool hasWon = false;
    public int enemiesLeft;
    public Vector2 minEnemySpawnBounds = new Vector2(-10f, 10f);
    public Vector2 maxEnemySpawnBounds = new Vector2(10f, -10f);
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
            currentWave++;
            if(currentWave == waveEnemyCount.Length - 1)
            {
                SceneManager.LoadScene("Win Screen");
            }
            else
            {
                SpawnEnemies();
            }
        }
    }
    private void Start()
    {
        currentWave = 0;
        
        if(SceneManager.GetActiveScene().name != "Main Menu")
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        StartCoroutine(ShowWaveText());
        enemiesLeft = waveEnemyCount[currentWave];
        for (int i = 0; i < waveEnemyCount[currentWave]; i++)
        {
            // Generate random x and z coordinates within bounds
            float randomX = Random.Range(minEnemySpawnBounds.x, maxEnemySpawnBounds.x);
            float randomZ = Random.Range(minEnemySpawnBounds.y, maxEnemySpawnBounds.y);

            // Create a new position using the random coordinates and fixed y-coordinate
            Vector3 randomPosition = new Vector3(randomX, yCoordinate, randomZ);

            Instantiate(enemy, randomPosition, Quaternion.identity);
        }
    }

    private IEnumerator ShowWaveText()
    {
        Debug.Log("Change wave text");
        waveCountText.text = "Wave " + (currentWave + 1);
        yield return new WaitForSeconds(4);
        waveCountText.text = " ";
    }
}
