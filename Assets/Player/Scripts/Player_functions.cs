using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_functions : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InputManager inputManager;

    // Update is called once per frame
    void Update()
    {
        inputManager.player_Mappings.PlayerInteract.Shoot.started += _ => Schoot();
    }

    private void Schoot()
    {
        Debug.Log("Shooting");
        //int layerMask = 1 << 8;
        //
        //RaycastHit hit;
        //
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        //{
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //    Debug.Log("Did Hit");
        //}
    }
}
