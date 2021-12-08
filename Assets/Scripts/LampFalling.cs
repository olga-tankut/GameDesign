using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampFalling : MonoBehaviour
{
    private SpriteRenderer sprender;
    private Rigidbody2D rb;
    private bool hasFallen;
    private bool canCollided;

    protected int damage = 30;


    // Start is called before the first frame update
    void Start()
    {
        sprender = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.Sleep();
        rb.isKinematic = true;
        hasFallen = false;
        canCollided = true;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void Fall()
    {
        if (hasFallen == false)
        {
            hasFallen = true;
            rb.WakeUp();
            rb.isKinematic = false;
            Debug.Log("be");   
        }    
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (canCollided == true)
        {
            canCollided = false;
            Debug.Log("fall");
            FindObjectOfType<AudioManager>().Play("Boom");  
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Fall();
        }
    }
}

