using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlimeController : MonoBehaviour {
    public float slimeMovement;
    public Rigidbody2D rb;
    public Transform playerTransform;

    public float movementSpeed = 1f;
    public float distance = 0;
    

    private MovementController _controller;

    private void Awake() {
        gameObject.AddComponent<MovementController>();
    }

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        _controller = GetComponent<MovementController>();

        // if no player is added
        if (playerTransform == null) {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }

    private void FixedUpdate() {
        if (Vector2.Distance(playerTransform.position, transform.position) < distance) {
            _controller.MoveTo(playerTransform);
        }
        else {
            _controller.MoveRandom();
        }
    }



}