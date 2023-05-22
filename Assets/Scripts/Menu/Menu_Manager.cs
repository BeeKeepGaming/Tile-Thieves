
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

/* Eugene van den berg
 * V1.7
 * Updated 07/04/2023
 */

public class Menu_Manager : MonoBehaviour
{
    Player_Controller playerController;
    InputAction pause;

    public GameObject pauseMenu;
    public bool hasPauseMenu = false;
    public static bool gameIsPaused = false;
    
    public GameObject[] optionalMenu;
    private int currentMenu;
    public static Menu_Manager instance;

    public Slider volumeSlider;
    public AudioSource audioSource;

    private void Awake()
    {        
        instance = this;
        playerController = new Player_Controller();
        volumeSlider.value = audioSource.volume;
    }

    private void OnEnable()
    {
        pause = playerController.UI.Pause;
        pause.Enable();
        pause.performed += PauseEvent;
    }

    private void OnDisable()
    {
        pause.performed -= PauseEvent;
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
        UnityEngine.Debug.Log("Quiting");
    }


    public void Resume()
    {
        pauseMenu.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void PauseEvent(InputAction.CallbackContext callback)
    {
        if(gameIsPaused == true)
        {
            Pause(false);
        }
        else
        {
            Pause(true);
        }
    }

    public void Pause(bool pState)
    {
        if(pState == true)
        {
            pauseMenu.SetActive(true);
            gameIsPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            gameIsPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void OptionalMenu(int optionalPanel)
    {
        optionalMenu[optionalPanel].SetActive(true);
        currentMenu= optionalPanel;
    }

    public void Back()
    {
        optionalMenu[currentMenu].SetActive(false);
    }

    public void SetVolume(float volume)
    {
        //Debug.Log("SetVolume called with volume: " + volume);
        audioSource.volume = volume;
    }
}
