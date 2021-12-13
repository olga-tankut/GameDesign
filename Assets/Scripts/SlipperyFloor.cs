using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyFloor : MonoBehaviour
{
    PlayerMovement player;
    Collider2D floorCollider;
    PlayerMovement pm;
    Rigidbody2D prb;
    GameObject p;

    public Vector2 acceleration = new Vector2(50f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        floorCollider = GetComponent<Collider2D>();
        p = GameObject.FindGameObjectWithTag("Player");
        pm = p.GetComponent<PlayerMovement>();
        prb = p.GetComponent<Rigidbody2D>();
        if (p == null) Debug.Log("Player missing, is player tagged?");

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        prb.GetComponent<Rigidbody2D>().velocity = pm.GetComponent<Rigidbody2D>().velocity * acceleration * Time.deltaTime;
    }
    protected void OnCollisionExit2D(Collision2D collision)
    {

    }
}
