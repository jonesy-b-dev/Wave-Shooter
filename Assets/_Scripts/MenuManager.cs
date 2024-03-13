using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private int currentSelection = 0;
    private int maxSelection;

    public GameManager gameManager;
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI retryText;

    [SerializeField]
    private Transform selectionImg;

    [SerializeField]
    private Transform[] button;

    [SerializeField]
    private string menuType; 

    private void Start()
    {
        maxSelection = button.Length - 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) ChangeSelection(-1);
        if (Input.GetKeyDown(KeyCode.S)) ChangeSelection(1);

        if (Input.GetButtonDown("Jump")) Select(currentSelection);
        selectionImg.position = new Vector2(button[currentSelection].position.x, button[currentSelection].position.y);

        try
        {
            if (gameManager.hasDied)
            {
                headerText.text = "You Died!";
                retryText.text = "Retry";
            }
            if (gameManager.hasWon)
            {
                headerText.text = "You Won!";
                retryText.text = "Play Again";
            }
        } 
        catch
        {
            var a = 1;
        }

    }
                                    
    private void ChangeSelection(int change)
    {
        if (currentSelection + change > maxSelection) currentSelection = 0;
        else if (currentSelection + change < 0) currentSelection = maxSelection;
        else currentSelection += change;
    }

    private void Select(int selection)
    {
        switch (menuType)
        {
            case "main":
                {
                    switch (selection)
                    {
                        case 0:
                            SceneManager.LoadScene("Level");
                            break;
                        case 1:
                            SceneManager.LoadScene("Options");
                            break;
                        case 2:
                            SceneManager.LoadScene("Credits");
                            break;
                        case 3:
                            Application.Quit();
                           break;
                    }
                } break;
            case "pause":
                {
                    switch (selection)
                    {
                        case 0:
                            {
                                if (!gameManager.hasDied && !gameManager.hasWon)
                                {
                                    gameManager.pauseMenu.SetActive(false);
                                    gameManager.isPaused = false;

                                    Time.timeScale = 1;
                                    Debug.Log("Unpaused");
                                } else
                                {
                                    Time.timeScale = 1;
                                    SceneManager.LoadScene("Level");
                                }
                            } break;
                        case 1:
                            {
                                Time.timeScale = 1;
                                SceneManager.LoadScene("MainMenu");
                            } break;
                    }
                } break;
            default: break;
        }
    }
}
