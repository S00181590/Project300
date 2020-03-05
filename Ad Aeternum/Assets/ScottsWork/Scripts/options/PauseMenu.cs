using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string sceneName;
    public GameObject pauseMenu;
    public bool IsPaused;
    public GameObject Options;
    public bool OptionsOpen;

    void Start()
    {
        IsPaused = false;
        OptionsOpen = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            OptionsOpen = false;
        }

        if (IsPaused)
        {
            //Instantiate(pauseMenu, transform);
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            Options.SetActive(false);
        }

        if (OptionsOpen)
        {
            Time.timeScale = 0f;
            Options.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            Options.SetActive(false);
        }
    }

    public void ResumeTheGame()
    {
        IsPaused = !IsPaused;
    }

    public void Optionsmenu()
    {

        OptionsOpen = !OptionsOpen;


        //if (OptionsOpen)
        //{
        //    Options.SetActive(true);
        //    Time.timeScale = 0f;
        //}
        //else if (!OptionsOpen)
        //{
        //    Options.SetActive(false);
        //    Time.timeScale = 1f;
        //}
    }

    public void RetuenToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void SaveSettings()
    {
        OptionsOpen = !OptionsOpen;

        //if (!OptionsOpen)
        //{
        //    Options.SetActive(false);
        //    Time.timeScale = 1f;
        //}
    }
}
