using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Player_Mappings player_Mappings;


    private void Awake()
    {
        player_Mappings= new Player_Mappings();

    }

    void OnEnable()
    {
        player_Mappings.Enable();
    }

    void OnDisable()
    {
        player_Mappings.Disable();
    }
}
