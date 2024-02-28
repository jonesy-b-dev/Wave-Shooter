using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float mouseSensitivity = 25f;
    [SerializeField] private Transform body;

    private float xRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;    
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = inputManager.player_Mappings.Camera.MouseX.ReadValue<float>() * mouseSensitivity * Time.deltaTime;

        float mouseY = inputManager.player_Mappings.Camera.MouseY.ReadValue<float>() * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Math.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        body.Rotate(Vector3.up * mouseX);
    }
}
