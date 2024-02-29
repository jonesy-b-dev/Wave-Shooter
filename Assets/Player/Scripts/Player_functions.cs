using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_functions : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float raycastDistance = 100f; // Distance for the raycast
    [SerializeField] private Transform cameraTransform; // Reference to the camera's transform

    void Start()
    {
        inputManager.player_Mappings.PlayerInteract.Shoot.started += _ => Shoot();
    }

    private void Shoot()
    {
        // Get the position and direction of the raycast
        Vector3 raycastOrigin = transform.position; // Assuming the ray starts from the player's position
        Vector3 raycastDirection = cameraTransform.forward; // Get the forward direction from the camera

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastDistance))
        {
            IInteractable testInterface = hit.collider.gameObject.GetComponent<IInteractable>();

            if (testInterface != null)
            {
                testInterface.Interact();
            }

            // If the ray hits something, visualize the ray and the hit point
            Debug.DrawLine(raycastOrigin, hit.point, Color.red, 0.1f);
        }
        else
        {
            // If the ray doesn't hit anything, visualize the ray to the maximum distance
            Vector3 endPosition = raycastOrigin + raycastDirection * raycastDistance;
            Debug.DrawLine(raycastOrigin, endPosition, Color.green, 0.1f);
        }
    }
}
