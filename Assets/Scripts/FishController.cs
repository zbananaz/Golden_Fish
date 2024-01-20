using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private static int nextFishID = 1;
    public int fishID;

    protected int maxHealthPoint;
    public int HealthPoint;
    public float percentageHP;
    public int damage;
    public int level;

    protected float y = -10;

    Collider2D[] foodColliders;
    Collider2D[] enemyColliders;

    [SerializeField] protected float moveSpeed = 80f;
    protected Vector2 dir;
    protected Vector2 dirToEnemy;
    protected float moveTime = 5f;

    protected float fallSpeed = 2f;
    protected bool isFalling = true;

    public Transform eatable;
    public Transform enemy;

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

    private void OnEnable()
    {
        fishID = nextFishID;
        nextFishID++;
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
        //Target();
        FindFood();
        FindEnemy();

        moveTime -= Time.deltaTime; // thoi gian doi huong di

        percentageHP = (float)HealthPoint / maxHealthPoint * 100;

        animator.SetFloat("hp", percentageHP);
        animator.SetFloat("level", level);
        if (HealthPoint <= 0)
        {
            animator.SetBool("die", true);
            Invoke(nameof(Die), 1.5f);
        }

        if (percentageHP < 100 && eatable != null)
        {
            dir = (eatable.position - transform.position).normalized;
        }
      
        if (eatable == null)
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
                Health(20);
            }
        }
    }

    public virtual int Health(int amount)
    {
        HealthPoint += amount;

        HealthPoint = Mathf.Clamp(HealthPoint, 0, maxHealthPoint);

        return HealthPoint;
    }

    List<Transform> checkFood;
    protected virtual Transform FindFood()
    {
        foodColliders = Physics2D.OverlapCircleAll(transform.position, 4f, LayerMask.GetMask("Food"));

        checkFood = foodColliders.Select(x => x.transform).ToList();

        if (checkFood.Count == 0) return eatable = null;

        else
        {
            eatable = checkFood.First();
            checkFood.ForEach(x =>
            {
                if ((transform.position - x.transform.position).sqrMagnitude < (transform.position - eatable.transform.position).sqrMagnitude)
                {
                    eatable = x;
                }
            });
            return eatable;
        }
    }

    List<Transform> checkEnemy;
    protected virtual Transform FindEnemy()
    {
        enemyColliders = Physics2D.OverlapCircleAll(transform.position, 10f, LayerMask.GetMask("Enemy"));

        checkEnemy = enemyColliders.Select(x => x.transform).ToList();

        if (checkEnemy.Count == 0) return enemy = null;

        else
        {
            enemy = checkEnemy.First();
            checkEnemy.ForEach(x =>
            {
                if ((transform.position - x.transform.position).sqrMagnitude < (transform.position - enemy.transform.position).sqrMagnitude)
                {
                    enemy = x;
                }
            });
            return enemy;
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
