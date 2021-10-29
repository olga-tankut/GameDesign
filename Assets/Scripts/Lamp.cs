using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    public float leftAngle;
    public float rightAngle;
    PlayerMovement player;

    bool moovingClockwise;
    Vector2 acceleration = new Vector2(1000000f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moovingClockwise = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveLamp();
    }

    public void ChangeMoveDir()
    {
        if(transform.rotation.z > rightAngle)
        {
            moovingClockwise = false;
        }
        if (transform.rotation.z < leftAngle)
        {
            moovingClockwise = true;
        }
    }

    public void MoveLamp()
    {
        ChangeMoveDir();
        if (moovingClockwise)
        {
            rb.angularVelocity = moveSpeed;
        }
        if (!moovingClockwise)
        {
            rb.angularVelocity = -1*moveSpeed;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
