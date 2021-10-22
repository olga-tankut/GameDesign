using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyFloor : MonoBehaviour
{
    public PlayerController player;
    Collider2D floorCollider;
    Vector2 acceleration = new Vector2(100f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        floorCollider = GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(player.GetComponent<Rigidbody2D>().velocity);
        //player.GetComponent<Rigidbody2D>().isKinematic = true;
        //player.GetComponent<Rigidbody2D>().transform.Translate(direction * Time.deltaTime * 100);
        //player.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity + acceleration; 

    }
    protected void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log(player.GetComponent<Rigidbody2D>().velocity);
        //player.GetComponent<Rigidbody2D>().isKinematic = false;
        //player.GetComponent<Rigidbody2D>().transform.Translate(direction * Time.deltaTime * 100);
        //player.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity + acceleration; 

    }
}
