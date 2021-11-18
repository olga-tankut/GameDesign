using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampFalling : MonoBehaviour
    
{
    public float countdown = 2;

    private SpriteRenderer sprender;
    private Rigidbody2D rb;
    private bool hasFallen = false;


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
        //countdown -= Time.deltaTime;
        //if (countdown <= 0f && !hasFallen)
        //{
        //    Fall();
        //}
       
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
        //if (collision.transform.gameObject)
        //{
        //    Debug.Log("Boom");
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Fall();
        }
    }
}

