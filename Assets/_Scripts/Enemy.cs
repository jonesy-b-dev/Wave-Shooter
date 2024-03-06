using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    // Start is called before the first frame update

    [SerializeField] private SOEnemy enemyStats;

    private new Renderer renderer;
    private float health;

    void Start()
    {
        health = enemyStats.health;
        renderer = GetComponent<Renderer>();
    }

    void IEnemy.Hit()
    {
        health = 0;

        if (health == 0 ) 
        {
            ((IEnemy)this).Death();
        }
        Debug.Log("Enemy Hit"); 
    }

    void IEnemy.Death()
    {
        Debug.Log("Dead");
     
        Destroy(gameObject);
    }
}
