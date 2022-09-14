using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
    float destroyNextTime;
    float speed = 0;
    float slow = 0;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        destroyNextTime = Time.time;
    }

    public void SetDestroyTime(float t)
    {
        destroyNextTime = Time.time + t + 1;
    }

    public void SetSpeed(float t)
    {
        speed = t;
        rb.velocity = transform.up * speed;
    }

    public void SetSlow(float t)
    {
        slow = t;
    }

    void Update()
    {
        if (Time.time >= destroyNextTime)
        {
            Destroy(this.gameObject);
        }
       speed -= slow;

        if (speed < 0) Destroy(this.gameObject);

      //  rb.velocity = transform.up * speed;
    }
}
