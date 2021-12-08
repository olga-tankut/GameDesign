using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    Collider2D fanCollider;
    PlayerMovement pm;
    Rigidbody2D prb;
    GameObject p;
    public float fanSpeed = 500;

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
        pm = PlayerMovement.Instance;
        prb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
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