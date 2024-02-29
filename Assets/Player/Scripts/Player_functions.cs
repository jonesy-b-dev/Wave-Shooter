using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_functions : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float raycastDistance = 1000f; // Distance for the raycast
    [SerializeField] private Transform cameraTransform;     // Reference to the camera's transform

    void Start()
    {
        inputManager.player_Mappings.PlayerInteract.Shoot.started += _ => Shoot();
    }

    private void Shoot()
    {
        // Get the position and direction of the raycast
        Vector3 raycastOrigin = transform.position; 
        Vector3 raycastDirection = cameraTransform.forward; // Get the forward direction from the camera

        if (Physics.Raycast(raycastOrigin, raycastDirection, out RaycastHit hit, raycastDistance))
        {
            IEnemy enemyInterface = hit.collider.gameObject.GetComponent<IEnemy>();

            // Check if interface is NULL
            enemyInterface?.Hit();

            // If the ray hits something, show the ray and the hit point
            Debug.DrawLine(raycastOrigin, hit.point, Color.red, 0.1f);
        }
        else
        {
            // If the ray doesn't hit anything, show the ray
            Vector3 endPosition = raycastOrigin + raycastDirection * raycastDistance;
            Debug.DrawLine(raycastOrigin, endPosition, Color.green, 0.1f);
        }
    }
}
