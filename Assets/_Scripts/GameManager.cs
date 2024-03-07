using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
// Serializable:
    [SerializeField] private GameObject enemy;
    [SerializeField] private int[] waveEnemyCount; 

// Public:
    public static GameManager instance;
    public float playerHealth = 100;

    private int currentWave;

    
    private void Awake()
    {
       if (instance != null && instance != this) Destroy(this);
       else instance = this;
    }

    private void Start()
    {
        currentWave = 0;
        for (int i = 0; i < waveEnemyCount[currentWave]; i++)
        {
            Instantiate(enemy);
        }
    }
}
