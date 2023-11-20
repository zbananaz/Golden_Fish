using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDeath : Enemy
{
    protected override void Start()
    {
        base.Start();
        damage = 0;
        attackRanged = 1.5f;
        attackDelay = 5f;
        moveSpeed = 60f;

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
        if(amount < 0)
        {
            animator.SetTrigger("takeDmg");
        }
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
        animator.SetTrigger("attack");
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        fish.gameObject.SetActive(false);
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