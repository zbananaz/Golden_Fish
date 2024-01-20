using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carnivore : FishController
{
    [SerializeField] GameObject weaponHolder;
    private GameObject megaShot;
    private float skillDelay = 1f;

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

        FindFood();
        FindEnemy();
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
    }

    private void Attack()
    {
        megaShot = ObjectPooling.instance.GetObjectFromPool("MegaShot");

        megaShot.transform.position = weaponHolder.transform.position;
        megaShot.SetActive(true);

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