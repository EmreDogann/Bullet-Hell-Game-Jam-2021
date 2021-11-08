using System;
using UnityEngine;

public class MachineGunnerAttack : MonoBehaviour {
    private Transform _playerTransform;
    public float range = 2;
    // Bullets per second.
    public float fireRate = 1;
    private float time = 0;

    // Start is called before the first frame update
    void Start() {
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        if (_playerTransform == null) {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }
        // If time to fire and enemy in range of player...
        if (time > fireRate && Vector2.Distance(transform.position, _playerTransform.position) < range) {
            ShootBullet();
            time = 0;
        } else {
            time += Time.deltaTime;
        }
    }

    void ShootBullet() {
        BulletManager.Spawn("Bullet", transform.position + transform.up, transform.rotation);
    }

    private void OnEnable() {
        if (gameObject.scene.name != "Managers") {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }
}