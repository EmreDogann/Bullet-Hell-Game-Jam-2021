using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider slider;

    private void Awake() {
        if (SceneManager.GetActiveScene().name == "MainMenu") {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void SetHealth(int health) {
        slider.value = health;
    }
}