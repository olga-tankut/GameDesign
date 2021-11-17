using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    Collider2D fanCollider;
    PlayerMovement pm;
    Rigidbody2D prb;
    GameObject p;
    public float fanSpeed = 1000;

    public enum Direction
    {
        up,
        right,
        down,
        left
    }
    public Direction direction = Direction.right;

    void Start()
    {
        fanCollider = GetComponent<Collider2D>();
        p = GameObject.FindGameObjectWithTag("Player");
        pm = p.GetComponent<PlayerMovement>();
        prb = p.GetComponent<Rigidbody2D>();
        if (p == null) Debug.Log("Player missing, is player tagged?");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Object entered trigger");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Object is in trigger");
        if (direction == Direction.up) prb.AddForce(Vector2.up * fanSpeed * Time.fixedDeltaTime);
        if(direction == Direction.right) prb.AddForce(Vector2.right * fanSpeed * Time.fixedDeltaTime);
        if (direction == Direction.down) prb.AddForce(Vector2.down * fanSpeed * Time.fixedDeltaTime);
        if (direction == Direction.left) prb.AddForce(Vector2.left * fanSpeed * Time.fixedDeltaTime);


    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Object left the trigger");
    }
}