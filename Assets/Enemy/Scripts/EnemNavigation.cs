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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.Find("Player");
        playerTransform = player.transform;
    }

    void Update()
    {
        directionToPlayer = playerTransform.position - transform.position;
        Vector3 destinationPosition = playerTransform.position - directionToPlayer.normalized * distanceFromPlayer;

        agent.destination = destinationPosition;
    }
}
