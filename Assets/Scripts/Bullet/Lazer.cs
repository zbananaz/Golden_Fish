using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float speed = 20;
    Rigidbody2D rb;


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Vector3 dir)
    {
        rb.velocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);

            gameObject.transform.rotation = Quaternion.identity;
            collision.GetComponent<Enemy>().Health(-damage);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
