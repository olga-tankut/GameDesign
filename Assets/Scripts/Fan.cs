using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    Collider2D fanCollider;
    PlayerMovement pm;
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Object entered trigger");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Fan Trigger entered");
        //Debug.Log(other.transform.tag);
        //Debug.Log(other.gameObject.name);
        if(other.transform.tag == "Player" && other.transform.name == "Scripts")
        {
            //Rigidbody2D prb = other.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D prb = other.gameObject.GetComponentInParent<Rigidbody2D>();

            if (direction == Direction.up) prb.AddForce(Vector2.up * fanSpeed * Time.fixedDeltaTime);
            if(direction == Direction.right) prb.AddForce(Vector2.right * fanSpeed * Time.fixedDeltaTime);
            if (direction == Direction.down) prb.AddForce(Vector2.down * fanSpeed * Time.fixedDeltaTime);
            if (direction == Direction.left) prb.AddForce(Vector2.left * fanSpeed * Time.fixedDeltaTime);
        }
        if (other.transform.tag == "Cardboard Box")
        {
            Rigidbody2D brb = other.gameObject.GetComponent<Rigidbody2D>();
            if (direction == Direction.up) brb.AddForce(Vector2.up * fanSpeed * Time.fixedDeltaTime);
            if (direction == Direction.right) brb.AddForce(Vector2.right * fanSpeed * Time.fixedDeltaTime);
            if (direction == Direction.down) brb.AddForce(Vector2.down * fanSpeed * Time.fixedDeltaTime);
            if (direction == Direction.left) brb.AddForce(Vector2.left * fanSpeed * Time.fixedDeltaTime);
        }


    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Object left the trigger");
    }
}