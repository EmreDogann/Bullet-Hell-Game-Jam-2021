using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        } else {
            // Starts the timer automatically
            timerIsRunning = true;
        }
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                LevelTransition levelTransition = Camera.main.GetComponent<LevelTransition>();
                levelTransition.nextLevel();
                timeRemaining = 10;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}", seconds);
    }
}