using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProwlerController : MonoBehaviour {
    // Player related properties
    public int findPlayerLocationInterval;
    public float transformOffsetFromPlayer;
    private Transform _playerTransform;
    private Vector2 _playerLastKnownPosition;
    private bool _isPlayerTransformNull;
    private float _findPLayerPositionTimer = 0;

    // preferably keep within melee range(which is default 2 )
    public float teleportRandomDeviation = 1.5f;

    // for random movements
    public float movementSpeed;


    // Movement controller
    private MovementController _controller;


    private void Awake() {
        gameObject.AddComponent<MovementController>();
        _controller = GetComponent<MovementController>();
    }

    // Start is called before the first frame update
    private void Start() {
        _isPlayerTransformNull = _playerTransform == null;
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {
        if (_playerTransform == null) {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            // _isPlayerTransformNull = false;
        }

        if (_findPLayerPositionTimer > 0) {
            _findPLayerPositionTimer -= Time.deltaTime;
        }
        else {
            // set it to behind the player
            // offset the teleporting position just behind the player
            _playerLastKnownPosition = _playerTransform.position;
            _playerLastKnownPosition -= (Vector2) (transformOffsetFromPlayer * _playerTransform.right);

            // randomise it a little
            _playerLastKnownPosition.x += Random.Range(-teleportRandomDeviation, teleportRandomDeviation);
            _playerLastKnownPosition.y += Random.Range(-teleportRandomDeviation, teleportRandomDeviation);

            // reset the timer
            _findPLayerPositionTimer = findPlayerLocationInterval;
        }
    }

    public void Teleport() {
        // go to player's last known location


        transform.position = _playerLastKnownPosition;
    }

    // private void FixedUpdate() {
    //     // add the player transform in if it isn't there
    //     if (_isPlayerTransformNull) {
    //         _playerTransform = GameObject.FindWithTag("Player").transform;
    //         _isPlayerTransformNull = false;
    //     }
    // }

    private void OnEnable() {
        if (gameObject.scene.name != "Managers") {
            // _isPlayerTransformNull = _playerTransform == null;
            _playerTransform = GameObject.FindWithTag("Player").transform;
            _isPlayerTransformNull = false;
        }
    }
}