using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaShot : MonoBehaviour
{
    public Transform target;
    [SerializeField] private int damage = 50;
    [SerializeField] private float speed = 10;


    private void Update()
    {
        target = gameObject.GetComponent<FishController>().enemy;
        if (target != null)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);

            gameObject.transform.rotation = Quaternion.identity;
            target.GetComponent<Enemy>().Health(-damage);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
