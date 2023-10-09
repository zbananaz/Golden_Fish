using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balrog : Enemy
{
    protected override void Start()
    {
        base.Start();
        damage = 40;
        attackRanged = 1.5f;
        attackDelay = 5f;

        maxHealthPoint = 100;
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

    protected override void Health(int amount)
    {
        base.Health(amount);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void Target()
    {
        base.Target();
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
