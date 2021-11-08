using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour {
    private HealthStat _playerHealth;
    private Transform _playerTransform;
    public int damage = 5;
    public float range = 2;

    // Start is called before the first frame update
    void Start() {
        _playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthStat>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        if (_playerTransform == null) {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }

    public void Attack() {
        // check if it meets the conditions
        if (Vector2.Distance(transform.position, _playerTransform.position) < range) {
            // Attack
            Debug.Log("ATTACK");
            _playerHealth.InflictDamage(damage);
        }
    }

    private void OnEnable() {
        if (gameObject.scene.name != "Managers") {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }

    private void OnDisable() {
        _playerTransform = null;
    }
}