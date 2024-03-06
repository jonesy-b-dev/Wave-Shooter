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
    [SerializeField] private float speed = 10f;
    [SerializeField] private float runSpeed = 15f;
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private float airSpeed = 0.3f;

//private:
    private Vector3 move;
    private float forward;
    private float right;
    private bool isColliding;
    private bool isGrounded;

    private void Start()
    {
        // Call jump function when jump button is pressed (lambda)
        inputManager.player_Mappings.Movement.Jump.started += _ => Jump();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded || isColliding)
        {
            // Read for the input
            forward = inputManager.player_Mappings.Movement.Forward.ReadValue<float>(); 
            right = inputManager.player_Mappings.Movement.Right.ReadValue<float>(); 
        }
        
        move = transform.right * right + transform.forward * forward;
        // Sprinting 
        move *= inputManager.player_Mappings.Movement.Run.ReadValue<float>() == 0 ? speed : runSpeed;
        
        // Crouching
        transform.localScale = new Vector3(1, inputManager.player_Mappings.Movement.Crouch.ReadValue<float>() == 0 ? 1f : 0.7f, 1);

        // Move the player
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }


    private void OnCollisionEnter(Collision other)
    {
        isColliding = true;

        forward = inputManager.player_Mappings.Movement.Forward.ReadValue<float>();
        right = inputManager.player_Mappings.Movement.Right.ReadValue<float>();

        // Check if player is colliding with the ground tag
        if (other.transform.CompareTag("Ground"))
        {
            //speed *= airSpeed;
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isColliding = false;

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
