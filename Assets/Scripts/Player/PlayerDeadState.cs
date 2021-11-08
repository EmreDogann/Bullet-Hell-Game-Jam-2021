using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeadState : MonoBehaviour {
    private HealthStat _healthStat;
    private bool deathTriggered = false;

    // Start is called before the first frame update
    void Start() {
        _healthStat = GetComponent<HealthStat>();
    }

    // Update is called once per frame
    void Update() {
        if (_healthStat.IsDead() && !deathTriggered) {
            Time.timeScale = 0f;
            deathTriggered = true;
            DeathScreen.desiredAlpha = 1;
        }
    }
}