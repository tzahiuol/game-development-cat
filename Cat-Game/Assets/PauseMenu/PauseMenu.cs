using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume() {
        print("Resume?");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        Timer timer = FindObjectOfType<Timer>();
        timer.Restart();

        ColliosionHandler ch = FindObjectOfType<ColliosionHandler>();
        ch.Restart();
        Resume();
    }

    public void Quit()
    {
        print("Quitting");
        Application.Quit();
    }
}

