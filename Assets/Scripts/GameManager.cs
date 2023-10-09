using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector2 leftEdge, rightEdge;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        leftEdge = Camera.main.ScreenToWorldPoint(Vector2.zero);
        rightEdge = Camera.main.ScreenToWorldPoint(new Vector2 (Screen.width, Screen.height));
    }
}
