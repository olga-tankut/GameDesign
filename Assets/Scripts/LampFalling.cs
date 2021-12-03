using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampFalling : MonoBehaviour
{
    private SpriteRenderer sprender;
    private Rigidbody2D rb;
    private bool hasFallen = false;

    protected int damage = 30;


    // Start is called before the first frame update
    void Start()
    {
        sprender = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.Sleep();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void Fall()
    {
        rb.WakeUp();
        rb.isKinematic = false;
        Debug.Log("wakeup");
        hasFallen = true;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Fall();
        }
    }
}

