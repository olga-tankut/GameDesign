using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTop : MonoBehaviour
    
{
    private SpriteRenderer sprender;
    private Rigidbody2D rb;
    private float countdown = 2;
    private bool hasFallen = false;


    // Start is called before the first frame update
    void Start()
    {
        sprender = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.Sleep();
        rb.isKinematic = true;
        sprender.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasFallen)
        {
            Fall();
        }
       
    }
    void Fall()
    {
        rb.WakeUp();
        sprender.enabled = true;
        rb.isKinematic = false;
        Debug.Log("wakeup");
        hasFallen = true;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject)
        {
            Debug.Log("Boom");
        }
    }
}

