using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public PlayerController PL;
    public Rigidbody2D rigidBody;
    public Collider2D regularCollider;
    public Collider2D slideCollider;

    public float slideSpeed = 5f;
    public bool isSliding = false;

    private void Update()
    {
        
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        prefromSlide();

    }
    private void prefromSlide()
    {
        
    }

    

}
