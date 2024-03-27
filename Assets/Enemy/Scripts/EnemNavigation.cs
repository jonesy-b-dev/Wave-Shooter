using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemNavigation : MonoBehaviour
{
// Public
    public Vector3 directionToPlayer; 

// Serializable:
    public Transform playerTransform;
    private float distanceFromPlayer = 10;

// Private:
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.Find("Player");
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        directionToPlayer = playerTransform.position - transform.position;
        Vector3 destinationPosition = playerTransform.position - directionToPlayer.normalized * distanceFromPlayer;

        agent.destination = destinationPosition;
    }
}
