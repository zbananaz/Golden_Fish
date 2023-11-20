using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    protected readonly float timer1 = 10f;

    protected float timeToDisable;

    public int coinValue = 10;

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
    }

    public void CollectCoin()
    {
        GameManager.Instance.bank += coinValue;
        GameManager.Instance.BankIncrease();
        gameObject.SetActive(false);
    }
}
