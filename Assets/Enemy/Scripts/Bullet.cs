using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
// Serialisable
    [SerializeField] private Rigidbody rb;

// public:
    public int force = 1000;

    void Start()
    {
        Vector3 directionToPlayer = GameManager.instance.player.transform.position - transform.position;
        rb.AddForce(directionToPlayer * force, ForceMode.Impulse);       
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision enter");
        Destroy(gameObject);
    }
}