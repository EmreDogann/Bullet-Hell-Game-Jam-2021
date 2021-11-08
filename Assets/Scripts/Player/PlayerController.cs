using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// basic player controller
public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5f;

    public float slowSpeed = 3f;

    private bool isSlowDown = false;
    public Vector2 directionMovement;

    public Animator animator;
    public Rigidbody2D rb;
    public Dash dash;

    private Camera _camera;
    private MovementController _controller;

    public GameObject gunPosition;

    private void Awake() {
        gameObject.AddComponent<MovementController>();
    }

    private void Start() {
        _camera = Camera.main;
        gameObject.GetComponent<HealthStat>().healthBar = GameObject.FindWithTag("PlayerHealthBar").GetComponent<HealthBar>();
        dash = GetComponent<Dash>();
        _controller = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update() {
        // if (!_camera) _camera = Camera.main;

        // Left Right
        directionMovement.x = Input.GetAxisRaw("Horizontal");
        // Up Down
        directionMovement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            dash.StartDashing(GetMouseDirection());
        }

        if (!PauseMenu.gameIsPaused) RotatePlayer();

        animator.SetFloat("Direction X", directionMovement.x);
        animator.SetFloat("Direction Y", directionMovement.y);

        // print(directionMovement.x);
        // print(directionMovement.y);
    }

    // rotate the player to face the mouse
    private void RotatePlayer() {
        gunPosition.transform.right = GetMouseDirection();
    }

    private void FixedUpdate() {
        // dont move towards if it is dashing
        if (!dash.GetIsDashing()) {
            if (isSlowDown) {
                _controller.MoveTowards(directionMovement, slowSpeed);
            } else {
                // Move the rigid body
                _controller.MoveTowards(directionMovement, moveSpeed);
            }
        }
    }

    public void SetIsSlow(bool slow) {
        isSlowDown = slow;
    }

    // As long as the player stays in contact with another collider...
    private void OnCollisionStay2D(Collision2D other) {
        // If the player collides with something while they are dashing, stop the dash.
        // Also, apply a small backwards force so the player doesn't get stuck.
        if (dash.GetIsDashing()) {
            dash.SetIsDashing(false);
            rb.AddForce(other.contacts[0].normal.normalized * 500, ForceMode2D.Force);
        }
    }


    private Vector2 GetMouseDirection() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector2 worldPosition = _camera.ScreenToWorldPoint(mousePos);
        return (worldPosition - (Vector2) transform.position).normalized;
    }
}