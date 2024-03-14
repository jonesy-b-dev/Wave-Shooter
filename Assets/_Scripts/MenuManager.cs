using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

enum Menus
{
    mainMenu,
    pauseMenu,
    credits,
    options
}

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;
    private GameObject mainMenu;
    private GameObject pauseMenu;
    private Menus menus;
    
    private void Awake()
    {
       if (instance != null && instance != this) Destroy(this);
       else instance = this;
    }

    private void Start()
    {
        UpdateScenes();
    }

    private void UpdateScenes()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Main Menu")
        {
            menus = Menus.mainMenu;
            mainMenu = GameObject.Find("Main Menu");
        }
        else
        {
            menus = Menus.pauseMenu;
            pauseMenu = GameObject.Find("Pause Menu");
        }
    }

    private void Update()
    {
    }
}
