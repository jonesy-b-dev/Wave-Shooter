using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
// Serialisable:
    [SerializeField] private SOEnemy enemyStats;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private GameObject bullet;

// Private
    private EnemNavigation enemNavigation;
    private GameObject player;
    private float health;

    void Start()
    {
        health = enemyStats.health;
        player = GameManager.instance.player;
        enemNavigation = GetComponent<EnemNavigation>();
    }

    void FixedUpdate()
    {
        int randomNumber = UnityEngine.Random.Range(0, 1000);

        if(randomNumber > 990)
        {
            ((IEnemy)this).Shoot();
        }
    }

    void IEnemy.Hit()
    {
        health = 0;

        if (health == 0 ) 
        {
            ((IEnemy)this).Death();
        }
    }

    void IEnemy.Death()
    {
        Debug.Log("Dead");

        GameManager.instance.EnemyDeath();
        
        Destroy(gameObject);
    }

    void IEnemy.Shoot()
    {
        Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);

        //Vector3 raycastOrigin = shootPoint.transform.position;
        //Vector3 directionToPlayer = (player.transform.position - raycastOrigin).normalized;

        //if(Physics.Raycast(raycastOrigin, directionToPlayer, out RaycastHit hit))
        //{
        //    Debug.DrawLine(raycastOrigin, hit.point, Color.red, 0.1f);
        //}
        //else
        //{
        //    Debug.DrawLine(raycastOrigin, hit.point, Color.green, 0.1f);
        //}
    }
}
