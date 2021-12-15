using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use this script to let a lamp fall and hit the player
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger enter");
        //Check if it's the player
        if (collision.gameObject.tag == "Player" && collision.gameObject.name == "Scripts")
        {
            Fall();
        }
    }

    void Fall()
    {
        if (hasFallen == false)
        {
            hasFallen = true;
            rb.WakeUp();
            rb.isKinematic = false;
        }    
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (canCollided == true)
        {
            canCollided = false;
            FindObjectOfType<AudioManager>().Play("Boom");  
        }   
    }
}

