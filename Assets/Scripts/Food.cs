using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    Animator animator;

    protected bool onGround = false;
    protected readonly float timer1 = 20f;

    protected float timeToDisable;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        timeToDisable = timer1;
    }

    protected virtual void Update()
    {
        if (onGround)
        {
            animator.SetBool("stop", true);
        }

        timeToDisable -= Time.deltaTime;
        if (timeToDisable <= 0)
        {
            gameObject.SetActive(false);
            timeToDisable = timer1;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            onGround = true;
        }

        if (collision.gameObject.CompareTag("Fish"))
        {
            collision.gameObject.GetComponent<FishController>().Health(20);
        }
    }
}
