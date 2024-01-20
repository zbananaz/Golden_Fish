using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSkill : MonoBehaviour
{
    public float lifetime = 1.5f; // Thời gian tồn tại của tia sét
    private float timer;

    private void OnEnable()
    {
        timer = lifetime;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            gameObject.SetActive(false); // Khi thời gian đã đủ, vô hiệu hóa tia sét
        }
    }
}
