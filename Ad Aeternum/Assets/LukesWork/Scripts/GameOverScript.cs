using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public HealthStaminaScript health;
    public Image gameOverImage;

    private static float prevRealTime;
    private float thisRealTime;

    void Start()
    {
        gameOverScreen.SetActive(false);
        gameOverImage.color = new Color(0, 0, 0, 0);
        Time.timeScale = 1;
    }
    
    void Update()
    {
        prevRealTime = thisRealTime;
        thisRealTime = Time.realtimeSinceStartup;

        if (health.value <= 0)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
        }

        if (gameOverScreen.activeSelf == true)
        {
            gameOverImage.color = Color.Lerp(gameOverImage.color, new Color(0, 0, 0, 1), gameOverDeltaTime);
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
    }

    public static float gameOverDeltaTime
    {
        get
        {
            if (Time.timeScale > 0f)
                return Time.deltaTime / Time.timeScale;

            return Time.realtimeSinceStartup - prevRealTime;
        }
    }
}
