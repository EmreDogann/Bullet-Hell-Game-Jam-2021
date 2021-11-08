using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !MainMenu.mainMenuLoaded) {
            if (gameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        // Resume game
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        // Pause game
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        gameIsPaused = false;
        EntitySpawner.Clear();
        LevelLoader.LoadSingle("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}