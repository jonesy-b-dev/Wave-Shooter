using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Rigidbody rb;

    [Header("Player Settings")]
    [SerializeField] private float speed = 10;
    [SerializeField] private float runSpeed = 15;
    [SerializeField] private float jumpForce = 200;

//private:
    private bool isGrounded;

    private void Start()
    {
        // Call jump function when jump button is pressed (lambda)
        inputManager.player_Mappings.Movement.Jump.started += _ => Jump();
    }

    // Update is called once per frame
    void Update()
    {
        // Read for the input
        float forward = inputManager.player_Mappings.Movement.Forward.ReadValue<float>(); 
        float right = inputManager.player_Mappings.Movement.Right.ReadValue<float>(); 
        
        // Setup the move vector
        Vector3 move = transform.right * right + transform.forward * forward;
        
        // Sprinting 
        move *= inputManager.player_Mappings.Movement.Run.ReadValue<float>() == 0 ? speed : runSpeed;
        
        // Crouching
        transform.localScale = new Vector3(1, inputManager.player_Mappings.Movement.Crouch.ReadValue<float>() == 0 ? 1f : 0.7f, 1);

        // Move the player
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }


    private void OnCollisionEnter(Collision other)
    {
        // Check if player is colliding with the ground tag
        if(other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        if(isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
