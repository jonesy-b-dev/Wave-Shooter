using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemNavigation : MonoBehaviour
{
// public:
    public Vector3 directionToPlayer; 
    public Transform playerTransform;

// Private:
    private NavMeshAgent agent;
    private float distanceFromPlayer = 10;
    private Enemy mainEnemyScript;

    void Start()
    {
        mainEnemyScript = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.Find("Player");
        playerTransform = player.transform;
    }

    void Update()
    {
        if(!mainEnemyScript.isDead)
        {
            directionToPlayer = playerTransform.position - transform.position;
            Vector3 destinationPosition = playerTransform.position - directionToPlayer.normalized * distanceFromPlayer;

            agent.destination = destinationPosition;
        }
    }
}
