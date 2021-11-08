using System;
using UnityEngine;
using Random = UnityEngine.Random;

// using Random = System.Random;

public class Bullet : MonoBehaviour {
    private HealthStat _playerHealth;
    private Vector3 _playerPos;
    public int damage = 1;
    public float bulletSpeed = 1;
    private Rigidbody2D rb;
    private bool _enabled;
    private float time;
    private float randomTime;

    // Start is called before the first frame update
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        randomTime = Random.Range(15, 25);
    }

    private void Start() {
        _playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthStat>();
    }

    private void Update() {
        time += Time.deltaTime;
        if (time > randomTime) {
            DespawnBullet();
        }
    }

    private void FixedUpdate() {
        if (_enabled) {
            rb.MovePosition((transform.position + transform.up * (bulletSpeed * Time.fixedDeltaTime)));
        }
    }

    void DespawnBullet() {
        _enabled = false;
        BulletManager.Despawn("Bullet", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _playerHealth.InflictDamage(damage);
            DespawnBullet();
        }
    }

    private void OnEnable() {
        _enabled = true;
        time = 0;
        if (gameObject.scene.name != "Managers") {
            _playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthStat>();
        }

        randomTime = Random.Range(15, 25);
    }
}