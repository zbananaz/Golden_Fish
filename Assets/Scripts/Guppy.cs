using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guppy : FishController
{
    private float timeToLevelUp = 30f;
    private float timeToSpawnCoin = 9f;

    private GameObject Coin;

    protected override void Start()
    {
        maxHealthPoint = 50;
        level = 0;
        base.Start();
    }

    protected override void MoveDirection()
    {
        base.MoveDirection();
    }

    protected override void Update()
    {
        base.Update();
        timeToLevelUp -= Time.deltaTime;
        timeToSpawnCoin -= Time.deltaTime;
        if (timeToLevelUp <= 0)
        {
            level += 1;
            timeToLevelUp = 30f;
        }

        if (timeToSpawnCoin <= 0)
        {
            SpawnCoin();
            timeToSpawnCoin = 9f;
        }

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public override void Health(int amount)
    {
        base.Health(amount);
    }

    protected override void Boundary()
    {
        base.Boundary();
    }

    protected override void Target()
    {
        base.Target();
    }

    protected override void Die()
    {
        base.Die();
    }

    private void SpawnCoin()
    {
        Vector2 spawnPos = transform.position;
        Coin = ObjectPooling.instance.GetObjectFromPool("Coin1");
        Coin.transform.position = spawnPos;
        Coin.SetActive(true);
        
    }
}

