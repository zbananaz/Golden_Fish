using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleMuncher : FishController
{
    [SerializeField] GameObject weaponHolder;
    private GameObject lazer;
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
                skillDelay = 0.5f;
            }
        }

        if (moveTime <= 0)
        {
            MoveDirection();
            moveTime = Random.Range(3f, 5f);
        }
    }

    private void Attack()
    {
        dirToEnemy = (enemy.position - transform.position).normalized;
        lazer = ObjectPooling.instance.GetObjectFromPool("Lazer");

        lazer.transform.position = weaponHolder.transform.position;
        lazer.SetActive(true);

        //set target
        lazer.GetComponent<Lazer>().SetTarget(dirToEnemy);

        //xoay vien dan
        lazer.transform.Rotate(0, 0, (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
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