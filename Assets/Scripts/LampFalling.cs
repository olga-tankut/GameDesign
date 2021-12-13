using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use this script to let a lamp fall and hit the player
public class LampFalling : MonoBehaviour
{
    private SpriteRenderer sprender;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        sprender = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.Sleep();
        rb.isKinematic = true;
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
        //Debug.Log("wakeup");
        rb.WakeUp();
        rb.isKinematic = false;
    }

    //protected void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Trigger Zone entered");
    //    if (collision.gameObject.tag == "Untagged" && collision.gameObject.name == "Scripts")
    //    {
    //        Fall();
    //    }
    //}

}

