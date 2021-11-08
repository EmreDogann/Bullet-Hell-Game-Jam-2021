using System;
using System.Collections;
using System.Collections.Generic;
using Timer;
using UnityEngine;

public class SlimeTrail : MonoBehaviour {
    public const string SlimeTrailTimerName = "Slime_Trail";

    // Start is called before the first frame update
    private Rigidbody2D rb;

    // private bool _enabled;
    //public SlimeMovement slime;

    public float lifetime;

    // private float timer;
    private Timers _timers;

    // private Timers timers1;

    private SpriteRenderer _sprite;

    public float slow;

    private void Awake() {
        _timers = gameObject.AddComponent<Timers>();
        _timers.AddTimer(SlimeTrailTimerName, lifetime);
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (_timers.UpdateTimer(SlimeTrailTimerName)) {
            Destroy();
        }
        var col = _sprite.color;
        col.a = _timers.GetTimerValue(SlimeTrailTimerName)/lifetime;
        _sprite.color = col;
    }

    void Destroy() {
        // _enabled = false;
        BulletManager.Despawn("Slime Trail", gameObject);
    }

    private void OnEnable() {
        // _enabled = true;
        _timers.ResetTimer(SlimeTrailTimerName);
    }

}