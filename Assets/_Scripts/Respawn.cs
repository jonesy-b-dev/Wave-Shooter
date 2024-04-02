using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
// Serialisable
    [SerializeField] private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        inputManager.player_Mappings.Respawn.Respawn.started += _ => Respawner();
    }

    private void Respawner()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
