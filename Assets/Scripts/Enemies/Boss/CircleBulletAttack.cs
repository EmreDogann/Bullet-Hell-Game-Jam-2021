using System;
using System.Collections;
using System.Collections.Generic;
using Timer;
using UnityEngine;

public class CircleBulletAttack : MonoBehaviour {
    // name for the timer
    public const string BulletFrequencyTimerName = "CircleBulletFrequency";
    public const string WaveResetFrequencyTimerName = "ResetWaveFrequency";
    
    public int numberOfBullets = 10;

    // number of waves to spawn and a counter
    public int wavesToSpawn = 1;
    private int waveCounter = 0;
    // the rotation difference from one wave to the next
    // in degrees
    public float waveAngleOffset = 45f;
    
    // frequency of the waves in seconds
    public float waveFrequency = 1f;

    public float wavesResetFrequency = 1f;
    
    private Timers _timers;

    // Start is called before the first frame update
    void Start() {
        _timers = GetComponent<Timers>();
        if (_timers == null) {
            _timers = gameObject.AddComponent<Timers>();
        }
        _timers.AddTimer(BulletFrequencyTimerName, waveFrequency);
        _timers.AddTimer(WaveResetFrequencyTimerName, wavesResetFrequency);
    }


    private void Update() {
        Attack();
    }

    public void Attack() {
        // angle of rotation for the bullet
        float anglePerBullet = 360f / numberOfBullets;

        if (_timers.UpdateTimer(BulletFrequencyTimerName)) {
            if (waveCounter < wavesToSpawn) {
                Quaternion bulletAngle = Quaternion.Euler(0f, 0f, 0f + waveAngleOffset * waveCounter);
                for (int i = 0; i < numberOfBullets; i++) {
                    Debug.Log(bulletAngle);
                    SpawnBullet(transform.position, bulletAngle);
                    bulletAngle.eulerAngles = new Vector3(0f, 0f, anglePerBullet + bulletAngle.eulerAngles.z);
                }

                waveCounter++;
            }
            _timers.ResetTimer(BulletFrequencyTimerName);
        }

        // only consider resetting the frequency after given number of waves are over
        if (waveCounter >= wavesToSpawn) {
            if (_timers.UpdateTimer(WaveResetFrequencyTimerName)) {
                waveCounter = 0;
                _timers.ResetTimer(WaveResetFrequencyTimerName);
            }
        }
    }

    void SpawnBullet(Vector2 position, Quaternion rotation) {
        BulletManager.Spawn("Circle Bullet", position, rotation);
    }
}