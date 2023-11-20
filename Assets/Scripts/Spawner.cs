using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject Guppy;
    private GameObject Food1;
    private Vector2 mousePos;

    void Update()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero);

        mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            if (hit2D.collider != null)
            {
                if (hit2D.collider.gameObject.CompareTag("Coin"))
                {
                    Coin coin = hit2D.collider.gameObject.GetComponent<Coin>();
                    if (coin != null)
                    {
                        coin.CollectCoin();
                    }
                }
                else
                {
                    SpawnFood();
                }
            }
            else
            {
                SpawnFood();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnFish(); 
        }
    }

    private void SpawnFish()
    {
        float spawnPosX = Random.Range(-8, 8);
        Vector2 spawnPosition = new(spawnPosX, 6);
        
        Guppy = ObjectPooling.instance.GetObjectFromPool("Guppy");
        Guppy.transform.position = spawnPosition;
        Guppy.SetActive(true);
        GameManager.Instance.fishes.Add(Guppy.GetComponent<FishController>();
        //GameManager.Instance.gameData.fishesRegister();
    }

    private void SpawnFood()
    {
        Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Food1 = ObjectPooling.instance.GetObjectFromPool("Food1");
        Food1.transform.position = spawnPosition;
        Food1.SetActive(true);

    }
}

//Remove ca'