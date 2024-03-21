using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;

//Serializable
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Volume postProcessing;
    private ColorAdjustments colorAdjustments;
 
//Private:
    private float pauseSaturation = -76.8f;
    private float normalSaturation = -36.3f;
    private ClampedFloatParameter pauseSat;
    private ClampedFloatParameter normalSat;


    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Main Menu")
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            postProcessing.profile.TryGet(out colorAdjustments);
        }

        pauseSat = new ClampedFloatParameter(pauseSaturation, -100f, 100f);
        normalSat = new ClampedFloatParameter(normalSaturation, -100f, 100f);

    }

    public void ShowPauseScreen()
    {

        GameManager.instance.isPaused = GameManager.instance.isPaused == false ? true : false;

        if (GameManager.instance.isPaused )
        {
            Debug.Log("Pause activated");
            Time.timeScale = 0f;

            colorAdjustments.saturation = pauseSat;
            Debug.Log(colorAdjustments.saturation);
            pauseMenu.SetActive(true);
        }
        else
        {
            Debug.Log("Pause deactivated");
            Time.timeScale = 1f;

            colorAdjustments.saturation = normalSat;
            Debug.Log(colorAdjustments.saturation);
            pauseMenu.SetActive(false);
        }

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
