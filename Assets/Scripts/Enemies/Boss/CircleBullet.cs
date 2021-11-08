using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour {
    public float speed = 10f;
    public int damage = 5;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        // constantly move towards a thing
        rb.MovePosition(rb.position + (Vector2) transform.right * (speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Destroy();
            other.GetComponent<HealthStat>().InflictDamage(damage);
            return; // exit this method
        }

        // if the layer is an obstacle destroy it as well
        if (((1<<other.gameObject.layer) & LayerMask.GetMask("Obstacle")) != 0) {
            Destroy();
        }
    }

    private void Destroy() {
        BulletManager.Despawn("Circle Bullet", gameObject);
    }
}