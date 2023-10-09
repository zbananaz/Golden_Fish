using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    protected int maxHealthPoint;
    protected int HealthPoint;
    protected int percentageHP;
    protected int damage;
    protected int level;

    protected float y = -10;
    protected float nearestFood;
    protected float nearestEnemy = -1;

    [SerializeField] protected float moveSpeed = 80f;
    [SerializeField] protected Vector2 dir;
    [SerializeField] protected float moveTime = 5f;

    [SerializeField] protected float fallSpeed = 2f;
    [SerializeField] protected bool isFalling = true;

    [SerializeField] protected GameObject enemy;
    protected GameObject eatable;

    protected bool die = false;

    // ====================================

    protected virtual void Start()
    {
        isFalling = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        MoveDirection();

        HealthPoint = maxHealthPoint;
    }

    protected virtual void MoveDirection()
    {
        float randomAngle = Random.Range(0, 360f);
        float angleInRadian = randomAngle * Mathf.Deg2Rad;

        float dirX = Mathf.Cos(angleInRadian);
        float dirY = Mathf.Sin(angleInRadian);

        dir = new(dirX, dirY);
    }

    protected virtual void Update()
    {
        
        FindTarget(); // tim do an va enemy

        moveTime -= Time.deltaTime; // thoi gian doi huong di

        percentageHP = (HealthPoint / maxHealthPoint) * 100;

        animator.SetFloat("hp", percentageHP);
        animator.SetFloat("level", level);
        if (enemy != null && nearestEnemy <= 2f)
        {
            dir =(transform.position - enemy.transform.position).normalized;
        }

        if (percentageHP < 100 && eatable != null)
        {
            dir = (eatable.transform.position - transform.position).normalized;
        }
        //if (enemy != null || eatable != null)
        //{
        //    if (enemy != null && nearestEnemy < 2f)
        //    {
        //        dir = (transform.position - enemy.transform.position) * 1.5f;
        //        Debug.Log("run");
        //    } else
        //    if (percentageHP < 100 && nearestFood < 3f)
        //    {
        //        dir = eatable.transform.position - transform.position;
        //        Debug.Log("eat");
        //    }
        //} else
        if (enemy == null && eatable == null)
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
        Boundary(); // gioi han be ca
        if (isFalling)
        {
            y += 3 * Time.fixedDeltaTime;
            rb.velocity = new Vector2(0, y);
            if(transform.position.y <= 1.5)
            {
                isFalling = false;
            }
        } else
        {
            rb.velocity = moveSpeed * Time.fixedDeltaTime * dir;
            if (rb.velocity.x > 0)
            {
                rb.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                rb.transform.rotation = Quaternion.Euler(0, 0, 0);

            }
        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            dir *= new Vector2(-Random.Range(1f, 1.5f), -Random.Range(-0.5f, 1.5f));
        }

        if (percentageHP < 100)
        {
            if (collision.CompareTag("Food"))
            {
                collision.gameObject.SetActive(false);
                animator.SetTrigger("eat");
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            Vector2 run = (transform.position - collision.gameObject.transform.position).normalized * 3f;
            rb.AddForce(run);
            //TODO đang ko run 
        }
    }

    public virtual void Health(int amount)
    {
        HealthPoint += amount;

        if (HealthPoint <= 0)
        {
            animator.SetBool("die", true);
            Invoke(nameof(Die), 1.5f);
        }
    }

     List<Transform> check;
    Transform nearest;
    public Transform FindTarget()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, 13f, LayerMask.GetMask("Enemy"));

        check = enemyColliders.Select(x => x.transform).ToList();
        nearest = check.First();
        check.ForEach(x =>
        {
            if ((transform.position - x.transform.position).sqrMagnitude < (transform.position - nearest.transform.position).sqrMagnitude)
            {
                nearest = x;
                enemy = x.gameObject;
                
            }
        });
        return nearest;
    }
    Collider2D[] enemyColliders;
        protected virtual void Target()
    {
        //float nearestFood = float.MaxValue;
        //float nearestEnemy = float.MaxValue;
        List<GameObject> Food = new();
        List<GameObject> Enemies = new();
        Collider2D[] foodColliders = Physics2D.OverlapCircleAll(transform.position, 3f, LayerMask.GetMask("Food"));
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, 13f, LayerMask.GetMask("Enemy"));
        check = enemyColliders.Select(x => x.transform).ToList();

        //=========================================
        // Food
        if (foodColliders.Length == 0)
        {
            eatable = null;
            return;
        }

        foreach (Collider2D c in foodColliders)
        {
            if (c.gameObject.CompareTag("Food"))
            {
                Food.Add(c.gameObject);
            }
        }

        foreach (GameObject t in Food)
        {
            float distance = Vector2.Distance(transform.position, t.transform.position);
            if (distance < nearestFood)
            {
                nearestFood = distance;
                eatable = t;
            }
        }

        //=========================================
        // Enemy
        if (enemyColliders.Length == 0)
        {
            enemy = null;
            return;
        }

        foreach (Collider2D e in enemyColliders) //enemies
        {
            if (e.gameObject.CompareTag("Enemy"))
            {
                Enemies.Add(e.gameObject);
            }
        }

        foreach (GameObject e in Enemies)
        {
            float distance = Mathf.Abs(Vector2.Distance(transform.position, e.transform.position));
            if (distance < nearestEnemy)
            {
                nearestEnemy = distance;
                enemy = e;
                Debug.Log("Distance: " + distance);
            }
        }
    }

    protected virtual void Boundary()
    {
        Vector3 fishPos = transform.position;
        fishPos.x = Mathf.Clamp(fishPos.x, GameManager.Instance.leftEdge.x + .5f, GameManager.Instance.rightEdge.x - .5f);
        fishPos.y = Mathf.Clamp(fishPos.y, GameManager.Instance.leftEdge.y + .5f, GameManager.Instance.rightEdge.y - .5f);
        transform.position = fishPos;
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
