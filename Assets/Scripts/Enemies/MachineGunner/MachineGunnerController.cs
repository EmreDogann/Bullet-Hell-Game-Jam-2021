using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MachineGunnerController : MonoBehaviour {
    // Player related properties
    public float findPlayerLocationInterval = 1f;
    public Transform _playerTransform;

    private float _findPLayerPositionTimer = 0;

    // for random movements
    public float movementSpeed;

    // Movement controller
    private MovementController _controller;

    private void Awake() {
        _controller = gameObject.AddComponent<MovementController>();
        movementSpeed -= Random.Range(0f, 1f);
    }

    // Start is called before the first frame update
    private void Start() {
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        if (_findPLayerPositionTimer > 0) {
            _findPLayerPositionTimer -= Time.deltaTime;
        } else {
            _controller.MoveTo(_playerTransform, movementSpeed);
            _findPLayerPositionTimer = findPlayerLocationInterval;
        }

        if (GetComponent<HealthStat>().IsDead()) {
            BulletManager.Despawn("Machine Gunner", gameObject);
        }
    }

    private void OnEnable() {
        if (gameObject.scene.name != "Managers") {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }
}