using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingGuppy : FishController
{
    private GameObject iceShard;
    private float skillDelay = .1f;

    protected override void Start()
    {
        level = 0;
        maxHealthPoint = 150;
        base.Start();
    }

    protected override void MoveDirection()
    {
        base.MoveDirection();
    }

    protected override void Update()
    {
        skillDelay -= Time.deltaTime;
        moveTime -= Time.deltaTime;

        if (enemy != null)
        {
            if (skillDelay <= 0)
            {
                Attack();
                skillDelay = 1.5f;
            }
        }

        if (moveTime <= 0)
        {
            MoveDirection();
            moveTime = Random.Range(3f, 5f);
        }

        base.Update();
    }

    private void Attack()
    {
        dirToEnemy = (enemy.position - transform.position).normalized;
        iceShard = ObjectPooling.instance.GetObjectFromPool("Ice Shard");

        iceShard.transform.position = transform.position;
        iceShard.SetActive(true);

        //set target
        iceShard.GetComponent<IceShard>().SetTarget(dirToEnemy);

        //xoay vien dan
        iceShard.transform.Rotate(0, 0, (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override Transform FindEnemy()
    {
        return base.FindEnemy();
    }

    protected override Transform FindFood()
    {
        return base.FindFood();
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

    protected override void Die()
    {
        base.Die();
    }
}