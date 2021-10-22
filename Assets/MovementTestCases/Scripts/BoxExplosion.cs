using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BoxExplosion : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Player")
        {
            if(Math.Abs(rb.velocity.y * rb.velocity.x) < 100 && Math.Abs(rb.velocity.y * rb.velocity.x) > 1.5f)
            {
                rb.AddForce(rb.velocity * 10.0f, ForceMode2D.Impulse);
                // rb.velocity *= 10.0f;
                boxCollider.enabled = false;
            }
            Debug.Log("" + rb.velocity + ", " + Math.Abs(rb.velocity.y * rb.velocity.x));
        }
    }
}
