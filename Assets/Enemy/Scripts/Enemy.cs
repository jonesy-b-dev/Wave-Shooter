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
    [SerializeField] private MeshCollider collider;

// Private
    private EnemNavigation enemNavigation;
    private GameObject player;
    private float health;
    private Rigidbody[] rigidbodies;
    public bool isDead = false;

    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        IsRagdoll(false);
        health = enemyStats.health;
        player = GameManager.instance.player;
        enemNavigation = GetComponent<EnemNavigation>();
    }

    void FixedUpdate()
    {
        if(!isDead)
        {
            int randomNumber = UnityEngine.Random.Range(0, 1000);

            if(randomNumber > 990)
            {
                ((IEnemy)this).Shoot();
            }
        }
    }

    void IEnemy.Hit()
    {
        Debug.Log("hit");
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
        IsRagdoll(true);

        //Destroy(gameObject);
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

    private void IsRagdoll(bool isRagdoll)
    {
        isDead = isRagdoll;
        collider.enabled = !isRagdoll;

        foreach(Rigidbody ragdollBone in rigidbodies)
        {
            ragdollBone.isKinematic = !isRagdoll;
        }
    }
}
