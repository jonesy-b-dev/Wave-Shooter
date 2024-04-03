using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
// Serialisable
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float mouseSensitivity = 8f;
    [SerializeField] private Transform body;

// private:
    private float xRotation = 0;

    // Update is called once per frame
    void LateUpdate()
    {
        float mouseX = inputManager.player_Mappings.Camera.MouseX.ReadValue<float>() * PlayerPrefs.GetFloat("Sensitivity") * Time.deltaTime;

        float mouseY = inputManager.player_Mappings.Camera.MouseY.ReadValue<float>() * PlayerPrefs.GetFloat("Sensitivity") * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Math.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        body.Rotate(Vector3.up * mouseX);
    }
}
