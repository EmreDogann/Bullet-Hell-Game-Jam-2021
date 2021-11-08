using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Parryable : MonoBehaviour {
    // if the object is in a parryable state
    // could have a tiny window in which you can parry
    [SerializeField] private bool _isParryable;

    // the origin of where this bullet was fired from for it to go back to
    public Vector2 origin;

    // speed of the bullet
    // maybe wise to get it from somewhere else and make this private
    public float speed;

    // a scaler for the speed so it is faster when parried
    public float parrySpeedScale = 1f;
    
    private bool _parryed;

    private MovementController _controller;

    // Start is called before the first frame update
    void Start() {
        // get the movement controller
        _controller = GetComponent<MovementController>();
    }

    private void FixedUpdate() {
        // /* DEBUGGING */
        // if (_isParryable)
        //     GetComponentInChildren<SpriteRenderer>().color = Color.red;
        // else
        //     GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        // /* END DEBUGGING */

        // if the bullet has been parried, move back to the origin
        if (_parryed) {
            OnParry();
        }
    }

    // can be overrided to provide specific functionality, by default it will go to the origin of the bullet
    protected virtual void OnParry() {
        Vector2 direction = (-(Vector2) transform.position + origin).normalized;
        Debug.Log("Direction: " + direction);
        _controller.MoveTowards(direction, speed * parrySpeedScale);
    }

    // the object requires a 2D Collider on a trigger
    public void OnTriggerEnter2D(Collider2D other) {
        if (_isParryable) {
            if (other.gameObject.CompareTag("Player")) {
                if (other.gameObject.GetComponent<Dash>().GetIsDashing()) {
                    // Debug.Log("Collided");
                    // start moving back to the origin
                    _parryed = true;
                }
            }
        }
    }

    public bool IsParryable() {
        return _isParryable;
    }

    public void SetIsParryable(bool value) {
        _isParryable = value;
    }
}