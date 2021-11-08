using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinBullet : MonoBehaviour
{
    private HealthStat bossHealth;
    private WaveAttack bossWaveAttack;

    private Transform playerTransform;

    // opacity of the bullet
    public float opacity;
    // the active bullet sprite whose opacity will be changed
    public SpriteRenderer activeBulletSprite;

    // the speed at which the opacity changes
    public float opacitySpeed = 0.5f;

    // keep between 0 and 1
    // if opacity is below this value then the player will not be damaged by it.
    public float threshold = 0.3f;

    public int damage = 0;

    public float bulletSpeed = 5f;
    public Vector2 bulletDirection;

    // a flipper to toggle between adding the speed value and decreasing it for the opacity
    private bool _toAdd;

    public int bulletIndex;

    private void Start()
    {
        bossHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<HealthStat>();
        bossWaveAttack = GameObject.FindGameObjectWithTag("Boss").GetComponent<WaveAttack>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // initialise the whether to add or to subtract the speed
        if (opacity < 0.5)
        {
            opacity = 0;
            _toAdd = true;
        }
        else
        {
            opacity = 1;
            _toAdd = false;
        }
    }
    public Vector2 LockYPos()
    {
        return new Vector2
       (playerTransform.position.x,  bossWaveAttack.getBulletY());

    }


    private void Update()
    {
        
        bulletSpeed = bossHealth.GetHealth() < 19 ? 10 : 5;

        if (_toAdd)
        {
            opacity += opacitySpeed * Time.deltaTime;
        }
        else
        {
            opacity -= opacitySpeed * Time.deltaTime;
        }

        if (opacity > 1)
        {
            _toAdd = false;
        }
        else if (opacity < 0)
        {
            _toAdd = true;
        }

        var col = activeBulletSprite.color;
        col.a = opacity;
        activeBulletSprite.color = col;
        //transform.position = Vector2.MoveTowards(transform.position, LockYPos(), 1f * Time.deltaTime);
        transform.position = LockYPos();
    }

    public bool CanDamage()
    {
        return opacity > threshold;
    }



    public void Explode()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CanDamage())
        {
            if (other.CompareTag("Player"))
            {
                // Debug.Log("DAs");
               other.GetComponent<HealthStat>().InflictDamage(damage);
            }
        }
    }
}