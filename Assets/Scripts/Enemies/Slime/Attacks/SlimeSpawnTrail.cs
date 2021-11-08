using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnTrail : MonoBehaviour {
    public GameObject bullet;

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating(nameof(SpawnSlimeTrial), 2, 0.5f);
    }

    void SpawnSlimeTrial() {
        BulletManager.Spawn("Slime Trail", transform.position, transform.rotation);
    }
}