using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeExplode : MonoBehaviour
{
    public Transform playerTransform;
    public HealthStat playerHealth;

    public float explodeDistance;

    public int explodeDamage;

    private bool _hasExploded = false;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        playerHealth = player.GetComponent<HealthStat>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasExploded == false)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < explodeDistance)
            {
                playerHealth.InflictDamage(explodeDamage);
                Destroy(gameObject);
                _hasExploded = true;
            }
        }
        animator.SetBool("isExplode", _hasExploded);
    }
}