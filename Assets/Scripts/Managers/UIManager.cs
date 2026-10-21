using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//<summary> This script handles UI Buttons! - Zander :3 <summary>
public class UIManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject pauseMenu;

    public bool isPaused;
    public bool isResumed;
    public bool isGameOver; //Will make it so you can't paused after GameOver is triggered

    private void Start()
    {
        isPaused = false;
        isGameOver = false;
        Time.timeScale = 1;
    }

    public void ChangeSceneButton(int sceneValue)
    {
        SceneManager.LoadScene(sceneValue); //sceneValue is handled within the Unity Editor
    }

    public void ToggleSettingsButton()
    {
        //This method can be used for both Settings button and return to Menu button
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void ToggleCreditsButton()
    {
        //This method can be used for both Credits button and return to Menu button
        creditsMenu.SetActive(!creditsMenu.activeSelf);
    }

    public void ToggleFullScreenButton()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    //Making seperate method as to not mess with resume button on pause menu
    public void PausePlayerInput(InputAction.CallbackContext context)
    {
        if(context.started) TogglePause();
    }

    //This method can be used for both ESC and a Resume Button, so not calling this method a button
    public void TogglePause()
    {
        if (!isGameOver)
        {
            isPaused = !isPaused; //Flips true or false
            pauseMenu.SetActive(!pauseMenu.activeSelf); //Toggles menu
            settingsMenu.SetActive(false); //Makes sure settings is always off when pausing or unpausing

            if (!isPaused) Time.timeScale = 1; //not Paused
            else Time.timeScale = 0; //Paused

            StartCoroutine(PreventAccidentalDrop());
        }
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    IEnumerator PreventAccidentalDrop()
    {
        isResumed = true;
        yield return new WaitForSeconds(.6f);
        isResumed = false;
    }
}