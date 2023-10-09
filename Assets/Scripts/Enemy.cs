using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly Animator animator;

    [SerializeField] protected float moveSpeed = 81f;
    protected float moveTime = 5f;

    protected int maxHealthPoint;
    protected int HealthPoint;
    protected int damage;

    protected bool isAttacking;
    protected float attackRanged;
    protected float attackDelay;
    protected float timeToAttack;

    [SerializeField] protected GameObject fish;

    protected Vector2 dir;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HealthPoint = maxHealthPoint;
        timeToAttack = 0;
        MoveDirection();
    }

    protected virtual void Update()
    {
        Target();
        Boundary();

        moveTime -= Time.deltaTime;
        timeToAttack -= Time.deltaTime;

        if (fish != null)
        {
            dir = fish.transform.position - transform.position;

            float distance = Vector2.Distance(transform.position, fish.transform.position);

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

    protected virtual void Health(int amount)
    {
        HealthPoint += amount;

        if (HealthPoint <= 0)
        {
            animator.SetBool("die", true);
            Invoke(nameof(Die), 1.5f);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            //collision.gameObject.GetComponent<FishController>().Health(-damage);
        }

        if (collision.CompareTag("Wall"))
        {
            dir *= new Vector2(-Random.Range(1f, 1.5f), -Random.Range(-0.5f, 1.5f));
        }
    }

    protected virtual void Target()
    {
        float nearestFish = float.MaxValue;
        List<GameObject> Fish = new();
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 15f, LayerMask.GetMask("Fish"));
        if (collider2Ds.Length == 0)
        {
            fish = null;
            return;
        }

        foreach (Collider2D c in collider2Ds)
        {
            if (c.gameObject.CompareTag("Fish"))
            {
                Fish.Add(c.gameObject);
            }
        }

        foreach (GameObject f in Fish)
        {
            float distance = Vector2.Distance(transform.position, f.transform.position);
            if (distance < nearestFish)
            {
                nearestFish = distance;
                fish = f;
            }
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
