using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Diagnostics.CodeAnalysis;

public class PlayerMovement : MonoBehaviour
{
    [Range(5, 15)]
    [SerializeField]private float jumpVelocity;
    [Range(1, 10)]
    [SerializeField]private float fallMultiplier = 2.5f;
    [Range(1, 5)]
    [SerializeField]private float lowJumpMultiplier = 2.0f;
    [Range(500, 3000)]
    [SerializeField]private float accelerationSpeed = 1000.0f;
    [Range(0, 15)]
    [SerializeField]private float maxGroundSpeed = 4f;
    [Range(0, 1000)]
    [SerializeField]private float breakForce = 100.0f;
    [Range(0, 5)]
    [SerializeField]private float dashCooldown = 3.0f;
    
    [Range(5, 15)]
    [SerializeField]private float movementEnergieConservationMultiplyer = 10f;
    [Range(0, 5)]
    [SerializeField]private float airMovementMultiplyer = 1.0f;
    [Range(0, 15)]
    [SerializeField]private float maxAirSpeed = 5.0f;
    [SerializeField]private bool multiJumpIsActive = false;
    [SerializeField]private bool airMovementIsActive = false;
    [Range(0, 10000)]
    [SerializeField]private float dashingLength = 1000;// length is determined by dashing time in ms
    [Range(0, 2000)]
    [SerializeField]private int dashBreakingStrength = 500;
    [Range(1, 20)]
    [SerializeField]private float dashingStrength = 10f;

    private Rigidbody2D rb;
    private Collision2D isInContactWithCollider;
    private float timeSinceLastDash = 0f;
    private float timeInDash = 0f;
    private bool isSliding = false;
    private bool isDashing = false;
    private float startingGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // beginning dash delay
        timeSinceLastDash = 3.1f;
        timeInDash = 0f;
        startingGravity = rb.gravityScale;
    }

    void Update()
    {
        if(IsOnGround() || multiJumpIsActive)
        {
            if(Input.GetButtonDown("Jump"))
            {
                // multi jumps are less strong because of rb.velocity
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity + rb.velocity;
            }
        }
        DashUpdate();
    }

    void FixedUpdate()
    {
        JumpFixedUpdate();
        SlideFixedUpdate();
        MoveFixedUpdate();
    }

    private void SlideFixedUpdate()
    {
        if(Input.GetKey(KeyCode.S))
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }
    }

    private void DashUpdate()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {   
            // startDash
            if(timeSinceLastDash > dashCooldown)
            {
                if(Input.GetKey(KeyCode.D))
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.right * dashingStrength, ForceMode2D.Impulse);
                    timeSinceLastDash = 0f;
                    // what if no direction is set?
                    if(!isDashing)
                    {
                        isDashing = true;
                        timeInDash = 0f;
                    }
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.left * dashingStrength, ForceMode2D.Impulse);
                    timeSinceLastDash = 0f;
                    // what if no direction is set?
                    if(!isDashing)
                    {
                    isDashing = true;
                    timeInDash = 0f;
                    }
                }
            }
            else
            {
                Debug.Log("Dash is not ready! Time until next dash: " + (dashCooldown - timeSinceLastDash) + " seconds.");
            }
        }

        // end dash
        if(isDashing)
        {
            // Breaking an reseting gravity
            if(timeInDash > dashingLength / 1000f / dashingStrength)
            {
                AddBreakingForce(dashBreakingStrength, Time.deltaTime);
                isDashing = false;
            }
            else
            {
                rb.gravityScale = 0;
            }
            
            timeInDash += Time.deltaTime;
        }
        else
        {
            rb.gravityScale = startingGravity;
        }

        timeSinceLastDash += Time.deltaTime;
    }

    private void MoveFixedUpdate()
    {
        // TODO: deaktivate stick to wall
        //movement
        float horizontalInput = Input.GetAxis("Horizontal");
        // acceleration is allowed to inrease in the opposite direction currently traveled
        if((Math.Abs(rb.velocity.x) < maxGroundSpeed && IsOnGround()) || (Math.Abs(rb.velocity.x) < maxAirSpeed && !IsOnGround()) ||
        (airMovementIsActive && (GetCurrentDirectionTraveledX() != (horizontalInput / Math.Abs(horizontalInput)))))
        {
            if(IsOnGround())
            {
                rb.AddForce(Vector2.right * horizontalInput * accelerationSpeed * Time.fixedDeltaTime);
            }
            else
            {
                rb.AddForce(Vector2.right * horizontalInput * accelerationSpeed * Time.fixedDeltaTime * airMovementMultiplyer);
            }
            
        }

        // slow down
        if(IsOnGround() && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            AddBreakingForce(breakForce, Time.fixedDeltaTime);
        }
    }

    // Calculates a breaking force to stop the gameObject
    // because of the scond parameter the function is time independent
    private void AddBreakingForce(float breakingStrength, float timeSinceLastFrameUpdate)
    {
        double x = Math.Sqrt(Math.Abs(rb.velocity.x));
        if(double.IsInfinity(x) || double.IsNaN(x))
            {   
                x = 0;
            }
            
        rb.AddForce(new Vector2((float)x * timeSinceLastFrameUpdate * breakingStrength * -(GetCurrentDirectionTraveledX()), 0));
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
        rb.AddForce(Vector2.right * horizontalInput * accelerationSpeed * Time.fixedDeltaTime * movementEnergieConservationMultiplyer);
    }

    // the return value is normalized
    private float GetCurrentDirectionTraveledX()
    {
        float temp = rb.velocity.x / Math.Abs(rb.velocity.x);
        if(float.IsNaN(temp))
        {
            temp = 0;
        }
        return temp;
    }
}
