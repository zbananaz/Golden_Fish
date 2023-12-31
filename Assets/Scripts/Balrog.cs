using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balrog : Enemy
{
    protected override void Start()
    {
        base.Start();
        damage = 10;
        attackRanged = 1.5f;
        attackDelay = 2f;
        moveSpeed = 85f;

        maxHealthPoint = 100;
        HealthPoint = maxHealthPoint;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void MoveDirection()
    {
        base.MoveDirection();
    }

    public override int Health(int amount)
    {
        return base.Health(amount);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override Transform FindTarget()
    {
        return base.FindTarget();
    }

    protected override void Attack()
    {
        base.Attack();
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
