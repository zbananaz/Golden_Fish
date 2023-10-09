using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    protected readonly float timer1 = 10f;

    protected float timeToDisable;

    protected virtual void Awake()
    {
        timeToDisable = timer1;
    }

    protected virtual void Update()
    {
        timeToDisable -= Time.deltaTime;
        if (timeToDisable <= 0)
        {
            gameObject.SetActive(false);
            timeToDisable = timer1;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero);

        //    if (hit2D.collider.CompareTag("Coin"))
        //    {
        //        // Xử lý hành động khi click vào coin ở đây
        //        hit2D.collider.gameObject.SetActive(false);
        //    }
        //}
    }
}
