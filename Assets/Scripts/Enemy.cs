using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    protected Animator animator;

    protected float moveSpeed;
    protected float moveTime = 5f;

    protected int maxHealthPoint;
    protected int HealthPoint;
    protected int damage;

    protected bool isAttacking;
    protected float attackRanged;
    protected float attackDelay;
    protected float timeToAttack;

    protected float percentageHP;

    Collider2D[] fishColliders;

    public Transform fish;

    protected Vector2 dir;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        HealthPoint = maxHealthPoint;
        timeToAttack = 0;
        MoveDirection();
    }

    protected virtual void Update()
    {
        //Target();
        FindTarget();
        Boundary();

        moveTime -= Time.deltaTime;
        timeToAttack -= Time.deltaTime;

        percentageHP = (float)HealthPoint / maxHealthPoint * 100;

        if (HealthPoint <= 0)
        {
            animator.SetBool("die", true);
            Invoke(nameof(Die), 1.5f);
            Die();
        }

        if (fish != null)
        {
            dir = fish.position - transform.position;

            float distance = Vector2.Distance(transform.position, fish.position);

            if (distance <= attackRanged && timeToAttack <= 0)
            {
                Attack();
                timeToAttack = attackDelay;
            }
        }
        else
        {
            if (moveTime <= 0)
            {
                MoveDirection();
                moveTime = Random.Range(3f, 5f);
            }
        }

        
    }

    protected virtual void FixedUpdate()
    {
        rb.velocity = moveSpeed * Time.fixedDeltaTime * dir.normalized;
        if (rb.velocity.x > 0)
        {
            rb.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            rb.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

    protected virtual void MoveDirection()
    {
        float randomAngle = Random.Range(0, 360f);
        float angleInRadian = randomAngle * Mathf.Deg2Rad;

        float dirX = Mathf.Cos(angleInRadian);
        float dirY = Mathf.Sin(angleInRadian);

        dir = new(dirX, dirY);
    }

    public virtual int Health(int amount)
    {
        HealthPoint += amount;

        HealthPoint = Mathf.Clamp(HealthPoint, 0, maxHealthPoint);

        return HealthPoint;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            dir *= new Vector2(-Random.Range(1f, 1.5f), -Random.Range(-0.5f, 1.5f));
        }
    }

    List<Transform> check;
    protected virtual Transform FindTarget()
    {
        fishColliders = Physics2D.OverlapCircleAll(transform.position, 14f, LayerMask.GetMask("Fish"));

        check = fishColliders.Select(x => x.transform).ToList();

        if (check.Count == 0) return fish = null;

        else
        {
            fish = check.First();
            check.ForEach(x =>
            {
                if ((transform.position - x.transform.position).sqrMagnitude < (transform.position - fish.transform.position).sqrMagnitude)
                {
                    fish = x;
                }
            });
            return fish;
        }

    }

    protected virtual void Attack()
    {
        Debug.Log("Attack");
        fish.GetComponent<FishController>().Health(-damage);
    }

    protected virtual void Boundary()
    {
        Vector2 enemyPos = transform.position;
        enemyPos.x = Mathf.Clamp(enemyPos.x, GameManager.Instance.leftEdge.x + .5f, GameManager.Instance.rightEdge.x - .5f);
        enemyPos.y = Mathf.Clamp(enemyPos.y, GameManager.Instance.leftEdge.y + .5f, GameManager.Instance.rightEdge.y - .5f);
        transform.position = enemyPos;
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);

        // die

    }
}
