using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GateKeeper : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private readonly int damage = 10;
    private int level;
    private readonly float moveSpeed = 80f;
    private float moveTime = 5f;
    private Collider2D[] enemyColliders;
    public Transform enemy;
    private GameObject LightingSkill;
    private float skillDelay = 1f;

    private Vector2 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        MoveDirection();
    }

    private void Update()
    {
        skillDelay -= Time.deltaTime;
        moveTime -= Time.deltaTime;

        FindTarget();
        if (enemy !=  null)
        {
            if (skillDelay <= 0)
            {
                Attack();
                skillDelay = 1f;
                dir = new((enemy.position - transform.position).x,0);
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

    private void FixedUpdate()
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
        int randomX = Random.Range(-1, 1);
        dir = new(randomX, 0);
    }

    List<Transform> check;
    private Transform FindTarget()
    {
        enemyColliders = Physics2D.OverlapCircleAll(transform.position, 20f, LayerMask.GetMask("Enemy"));

        check = enemyColliders.Select(x => x.transform).ToList();

        if (check.Count == 0) return enemy = null;

        else
        {
            enemy = check.First();
            check.ForEach(x =>
            {
                if ((transform.position - x.transform.position).sqrMagnitude < (transform.position - enemy.position).sqrMagnitude)
                {
                    enemy = x;
                }
            });
            return enemy;
        }

    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        StartCoroutine(WaitForAnimation());

        //LightingSkill = ObjectPooling.instance.GetObjectFromPool("Lighting skill");
        //LightingSkill.transform.position = new(enemy.position.x, enemy.position.y + 3f);
        //LightingSkill.SetActive(true);
        //enemy.GetComponent<Enemy>().Health(-damage);
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        LightingSkill = ObjectPooling.instance.GetObjectFromPool("Lighting skill");
        if (enemy != null)
        {
            LightingSkill.transform.position = new(enemy.position.x, enemy.position.y + 2f);
            LightingSkill.SetActive(true);
            enemy.GetComponent<Enemy>().Health(-damage);
        }
        
    }
}
