using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPosition : MonoBehaviour
{
    public GameObject wallLeftPos;
    public GameObject wallRightPos;
    public GameObject wallUpPos;
    public GameObject wallDownPos;

    private void Start()
    {
        wallLeftPos.transform.position = new(GameManager.Instance.leftEdge.x, 0);
        wallRightPos.transform.position = new(GameManager.Instance.rightEdge.x, 0);
        wallUpPos.transform.position = new(0, GameManager.Instance.leftEdge.y);
        wallDownPos.transform.position = new(0, -GameManager.Instance.leftEdge.y);
    }
}
