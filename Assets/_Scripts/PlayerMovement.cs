using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;
    public Rigidbody rb;

    [SerializeField] private float speed = 10;
    [SerializeField] float runSpeed = 15;
    [SerializeField]public float jumpForce = 200;

    private bool isGrounded;

    private void Start()
    {
        inputManager.player_Mappings.Movement.Jump.started += _ => Jump();
    }

    // Update is called once per frame
    void Update()
    {
       float forward = inputManager.player_Mappings.Movement.Forward.ReadValue<float>(); 
       float right = inputManager.player_Mappings.Movement.Right.ReadValue<float>(); 
       Vector3 move = transform.right * right + transform.forward * forward;

       move *= inputManager.player_Mappings.Movement.Run.ReadValue<float>() == 0 ? speed : runSpeed;
       transform.localScale = new Vector3(1, inputManager.player_Mappings.Movement.Crouch.ReadValue<float>() == 0 ? 1f : 0.7f, 1);

       rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    private void OnCollisionEnter(Collision other)
    {
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
