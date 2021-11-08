using System;
using UnityEngine;

public class Dash : MonoBehaviour {
    public Rigidbody2D rb;
    public float dashSpeed = 20f;
    public float maxDistance = 5f;

    public bool _isDashing = false;
    private Vector2 _direction;
    private Vector2 _preDashPosition;
    private MovementController _controller;

    private void Start() {
        _controller = GetComponent<MovementController>();
    }

    public void Update() {
        // dashing
        if (_isDashing) {
            if (Vector2.Distance(_preDashPosition, transform.position) >= maxDistance)
                _isDashing = false;
            else _controller.MoveTowards(_direction, dashSpeed);
        }
    }

    public void StartDashing(Vector2 direction) {
        // don't dash if currently dashing
        if (_isDashing)
            return;
        
        _isDashing = true;
        _preDashPosition = transform.position;
        _direction = direction;
    }

    public void SetIsDashing(bool dashing) {
        _isDashing = dashing;
    }

    public void SetPreDashPosition(Vector2 newPos) {
        _preDashPosition = newPos;
    }
    
    public Vector2 GetPreDashPosition() {
        return _preDashPosition;
    }

    public bool GetIsDashing() {
        return _isDashing;
    }
}