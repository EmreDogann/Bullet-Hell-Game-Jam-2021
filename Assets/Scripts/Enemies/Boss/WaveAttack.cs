using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack : MonoBehaviour
{
    // sin bullet prefab
    public GameObject bullet;

    public int amountToSpawn;
    // time in seconds
    public float timeToLoadBullets = 2f;
    private float _timePerBullet;
    private int _currentNumber;
    private List<float> bulletYOfsets;
    private Vector2 waveCentre;
    private int bulletCounter;
    private void Awake()
    {
        _timePerBullet = timeToLoadBullets / amountToSpawn;
    }

    // Start is called before the first frame update
    private void Start()
    {
        bulletYOfsets = new List<float>();
        waveCentre =new Vector2(0, 0);
    }

    public float getBulletY()
    {
        if (bulletYOfsets.Count == 0)
        {
            return 0;
        }
        if (bulletCounter == bulletYOfsets.Count)
        {
            bulletCounter = 0;
        }
        var y = bulletYOfsets[bulletCounter];
        bulletCounter++;
        return y + waveCentre.y;
    }

    private void Update()
    {
        if (_currentNumber == amountToSpawn)
        {

        }

        if (_currentNumber > amountToSpawn)
        {
            _currentNumber = amountToSpawn;
        }

        if (_currentNumber < amountToSpawn)
        {
            var pos = transform.position;
            pos.y += (float)_currentNumber / 2;
            pos.x = (float)Mathf.Sin(_timePerBullet * (_currentNumber + 1) * Mathf.Rad2Deg);

            if (!(_currentNumber > amountToSpawn / 2))
            {
                if (pos.x < 0)
                {
                    pos.x *= -1;
                }
            }
            else
            {
                if (pos.x > 0)
                {
                    pos.x *= -1;
                }
            }
            bulletYOfsets.Add(pos.y);
            SpawnBullet(pos, transform.rotation);
            _currentNumber += 1;
        }
    }

    public void SpawnBullet(Vector2 position, Quaternion rotation)
    {
        BulletManager.Spawn("Sin Bullet", position, rotation);
    }
}