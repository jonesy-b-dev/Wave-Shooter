using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
//Serializable:
    [Header("Components")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform groundCheckTransform;
    [Space(5)]
    [Header("Player Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float runSpeed = 15f;
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [Space(5)]
    [Header("Other")]
    [SerializeField] private AudioClip[] footsteps;

//private:
    private Vector3 move;
    private float forward;
    private float right;
    private bool isColliding;
    private bool isGrounded;
    private float stepDelta;

    private void Start()
    {
        // Call jump function when jump button is pressed (lambda)
        inputManager.player_Mappings.Movement.Jump.started += _ => Jump();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
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
        
        if(move != new Vector3(0, 0, 0) && isGrounded)
        {
            stepDelta += inputManager.player_Mappings.Movement.Run.ReadValue<float>() == 0 ? Time.deltaTime : Time.deltaTime * 2;
        }

        if(stepDelta > 0.5)
        {
            audioManager.PlayAudio(footsteps[UnityEngine.Random.Range(0, footsteps.Length)], transform.position);
            stepDelta = 0;

        }
    
    }

    private void CheckGrounded()
    {
        if (Physics.Raycast(groundCheckTransform.position, Vector3.down, groundCheckDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;

        forward = inputManager.player_Mappings.Movement.Forward.ReadValue<float>();
        right = inputManager.player_Mappings.Movement.Right.ReadValue<float>();

        // Check if player is colliding with the ground tag
        if (collision.transform.CompareTag("Ground"))
        {
            //speed *= airSpeed;
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;

        if (collision.transform.CompareTag("Ground"))
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
