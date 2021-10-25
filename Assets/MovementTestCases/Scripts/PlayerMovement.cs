using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PlayerMovement : MonoBehaviour
{
    [Range(5, 15)]
    [SerializeField]private float jumpVelocity;
    [Range(1, 5)]
    [SerializeField]private float fallMultiplier = 2.5f;
    [Range(1, 5)]
    [SerializeField]private float lowJumpMultiplier = 2.0f;
    [Range(500, 3000)]
    [SerializeField]private float accelerationSpeed = 1000.0f;
    [Range(1, 10)]
    [SerializeField]private float dashStrength = 3.0f;
    [Range(2, 10)]
    [SerializeField]private float maxSpeed = 4f;
    [Range(2, 10)]
    [SerializeField]private float breakForce = 5.0f;
    [Range(0, 5)]
    [SerializeField]private float dashCooldown = 3.0f;
    [SerializeField]private bool multiJumpIsActive = false;
    [Range(5, 15)]
    [SerializeField]private float MovementEnergieConservationMultiplyer = 10f;

    private Rigidbody2D rb;
    private Collision2D isInContactWithCollider;
    private float timeSinceLastDash = 0f;
    private Vector2 energyConservation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeSinceLastDash = 3.1f;
        energyConservation = new Vector2(0, 0);
    }

    void Update()
    {
        if(IsOnGround() || multiJumpIsActive)
        {
            if(Input.GetButtonDown("Jump"))
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity + rb.velocity;
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Debug.Log(rb.velocity);
            // Dash in the current moving Direction
            //rb.AddForce(rb.velocity.normalized * dashStrength, ForceMode2D.Impulse);
            if(timeSinceLastDash > dashCooldown)
            {
                if(Input.GetKey(KeyCode.D))
                {
                    rb.AddForce(Vector2.right * dashStrength, ForceMode2D.Impulse);
                    timeSinceLastDash = 0f;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    rb.AddForce(Vector2.left * dashStrength, ForceMode2D.Impulse);
                    timeSinceLastDash = 0f;
                }
            }
            
            //switch(Input.GetKey)
        }
        timeSinceLastDash += Time.deltaTime;
    }

    void FixedUpdate()
    {
        JumpFixedUpdate();
        MoveFixedUpdate();
    }

    private void MoveFixedUpdate()
    {
        // TODO: deaktivate stick to wall
        //movement
        float horizontalInput = Input.GetAxis("Horizontal");
        if(Math.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(Vector2.right * horizontalInput * accelerationSpeed * Time.fixedDeltaTime);
        }

        // gegen force zum bremsen
        if(IsOnGround())
        {
            //ToDo: doesn't work at the moment friction physics material is needed
            rb.AddForce(Vector2.right * breakForce * rb.velocity * Time.fixedDeltaTime);
        }
        
    }

    private void JumpFixedUpdate()
    {   // Quelle: https://www.youtube.com/watch?v=7KiK0Aqtmzc zitat 1
        // code zitat: 1
        if(rb.velocity.y < 0)
        {
            float jumpVelocity = rb.velocity.y + (Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
        // ende code zitat: 1
        else if(IsOnGround() || multiJumpIsActive)
        {
            // code zitat: 
            if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
            // ende code zitat: 1
        }
    }

    // ToDo: detecton of conntact 
    // walljumps are possible
    private bool IsOnGround()
    {
        // needs maybe safety margin
        // does accept all colliders
        if(rb.velocity.y >= 0 && isInContactWithCollider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        isInContactWithCollider = collisionInfo;
        addConservedEnergyToMomentum();
    }

    private void OnCollisionExit2D(Collision2D collisionInfo)
    {
        isInContactWithCollider = null;
    }

    private void addConservedEnergyToMomentum()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.AddForce(Vector2.right * horizontalInput * accelerationSpeed * Time.fixedDeltaTime * MovementEnergieConservationMultiplyer);
    }
}
