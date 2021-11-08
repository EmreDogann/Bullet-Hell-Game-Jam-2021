using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("LaunchProjectile", 0.0f, 0.5f);
    }

    void LaunchProjectile()
    {
        BulletManager.Spawn("Bullet", player.transform.position, player.transform.rotation);
    }
}
