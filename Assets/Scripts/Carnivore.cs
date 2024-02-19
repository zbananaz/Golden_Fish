using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carnivore : FishController
{
    private GameObject megaShot;
    private float skillDelay = .1f;

    protected override void Start()
    {
        level = 0;
        maxHealthPoint = 200;
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
                skillDelay = 2f;
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
        megaShot = ObjectPooling.instance.GetObjectFromPool("MegaShot");

        megaShot.transform.position = transform.position;
        megaShot.SetActive(true);

        //set target
        megaShot.GetComponent<MegaShot>().SetTarget(dirToEnemy);

        //xoay vien dan
        megaShot.transform.Rotate(0, 0, (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
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

    protected override Transform FindFood()
    {
        return base.FindFood();
    }

    protected override Transform FindEnemy()
    {
        return base.FindEnemy();
    }

    protected override void Die()
    {
        base.Die();
    }
}