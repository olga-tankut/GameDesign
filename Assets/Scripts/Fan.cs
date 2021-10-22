using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    Collider2D fanCollider;
    public PlayerController player;
    public float fanSpeed;
    
    void Start()
    {
        fanCollider = GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object entered trigger");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Object is in trigger");
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * fanSpeed * Time.deltaTime);

    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Object left the trigger");
    }
}
