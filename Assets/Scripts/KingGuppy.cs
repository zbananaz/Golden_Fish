using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingGuppy : FishController
{
    public float timeToSpawnCoin = 9f;

    private GameObject Coin;

    protected override void Start()
    {
        level = 0;
        maxHealthPoint = 100;
        base.Start();
    }

    protected override void MoveDirection()
    {
        base.MoveDirection();
    }

    protected override void Update()
    {
        base.Update();
        timeToSpawnCoin -= Time.deltaTime;
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

    public override int Health(int amount)
    {
        return base.Health(amount);
    }

    protected override void Boundary()
    {
        base.Boundary();
    }

    protected override Transform FindTarget()
    {
        return base.FindTarget();
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