using System;
using UnityEngine;

namespace Player {
    public class PlayerShooting : MonoBehaviour {
        public float fireRate = 0.1f;
        private float fireTimer = 0f;
        private bool isFiring = false;

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("Down");
                isFiring = true;
            }

            if (Input.GetMouseButtonUp(0)) {
                
                Debug.Log("Up");
                isFiring = false;
                fireTimer = 0f;
            }

            if (isFiring) {
                if (fireTimer > 0) {
                    fireTimer -= Time.deltaTime;
                }
                else {
                    SpawnBullet(transform.position, transform.rotation);
                    fireTimer = fireRate;
                }
            }
        }

        void SpawnBullet(Vector2 position, Quaternion rotation) {
            BulletManager.Spawn("Player Bullet", position, rotation);
        }
    }
}