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
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsMenu;
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
        }
        else
        {
            menus = Menus.pauseMenu;
        }
    }

    private void Update()
    {
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene("GameScene1");
    }

    public void OnPauseClick()
    {

    }
    public void OnResumeClick()
    {

    }
    public void OnCreditsClick()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    
    public void OnBackClick(string whatBack)
    {
        switch (whatBack)
        {
            case "credits":
                creditsMenu.SetActive(false);
                mainMenu.SetActive(true);
            break;
        }
    }
    public void OnOptionsClick()
    {

    }
    public void OnExitClick()
    {
        Application.Quit();
    }
}
