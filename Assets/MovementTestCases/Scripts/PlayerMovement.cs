using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

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
    [Range(0, 5000)]
    [SerializeField]private float dashCooldown = 1000f;// in ms
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
    [Range(0, 2)]
    [SerializeField]private float slidingLength = 1;// it is not proportional
    [SerializeField]private bool wallJumpIsAktive = true;
    [Range(0, 2000)]
    [SerializeField]private int ForgivingFramesWallJump = 500;
    [Range(0, 2000)]
    [SerializeField]private int ForgivingFramesDashCancel = 300;
    [Range(0, 10)]
    [SerializeField]private float wallJumpStrength = 5f;
    [Range(0, 3)]
    [SerializeField]private float upwardForceOnSlopes = 1.0f;
    
    //public Animator animator;

    private Rigidbody2D rb;
    private Collision2D isInContactWithCollider;
    private float timeSinceLastDash = 0f;
    private float timeInDash = 0f;
    private static bool isSliding = false;
    private static bool isDashing = false;
    private static bool isJumping = false;
    private float startingGravity;
    private float slideMultiplyer = 1.0f;
    private long timeSinceLastContactWithWall = 0; // in ms
    private long timeSinceStartOfDash = 0;
    private Vector2 wallWithLastContactPosition;
    private ContactPoint2D[] colliderCurrentlyinContactWith = new ContactPoint2D[5];
    private GameObject raycastStart;
    private long slopeDelay = 5; // ms
    private long timeOfHitOfSlope;
    private long timeOfStartOfFall;
    private long fallAnimationDelayOnSlopes = 5; // ms
    private Vector3 raycastStartlocalPosition;
    private Vector3 playerScaling;
    private CapsuleCollider2D collider;
    private Vector2 colliderSize;
    private long startingTimeOfSlide = 0;
    private Vector2 startingVelocityOfSlide;


    private int counter = 0;
    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        // beginning dash delay => has to be bigger than dashCooldown to be instant available at the start of the game
        timeSinceLastDash = (dashCooldown / 1000) + 0.1f;
        timeInDash = 0f;
        startingGravity = rb.gravityScale;
        slideMultiplyer = 1.0f;
        timeSinceLastContactWithWall = ForgivingFramesWallJump + 1;
        timeOfHitOfSlope = 0; // 1970-01-01 00:00
        timeOfStartOfFall = 0;
        raycastStartlocalPosition = transform.Find("RayCastStart").transform.localPosition;
        playerScaling = transform.parent.transform.localScale;
        collider = GetComponent<CapsuleCollider2D>();
        colliderSize = collider.size; // starting horizontal
        startingVelocityOfSlide = Vector2.zero;

    }

    void Update()
    {
        if(IsOnGround() || multiJumpIsActive || IsOnSlope())
        {
            if(Input.GetButtonDown("Jump"))
            {

                isJumping = true;
                // multi jumps are less strong because of rb.velocity
                rb.velocity = Vector2.up * jumpVelocity + rb.velocity;
            }
        }
        DashUpdate();
    }

    void FixedUpdate()
    {
        WallJumpFixedUpdate();
        JumpFixedUpdate();
        SlideFixedUpdate();
        MoveFixedUpdate();
        // cancel Jumping animation on slopes
        if(IsOnSlope() && !Input.GetKey(KeyCode.Space))
        {
            isJumping = false;
        }
        if(counter == 2)
        {
            Debug.Log("slide: " + isSliding + ", Slope: " + IsOnSlope());
            counter = 0;
        }
        else
        {
            counter++;
        }
    }

    private void WallJumpFixedUpdate()
    {
        if(ForgivingFramesWallJump > (DateTimeOffset.Now.ToUnixTimeMilliseconds() - timeSinceLastContactWithWall) 
            && Input.GetKeyDown(KeyCode.Space) && !IsOnGround() && wallJumpIsAktive)
        {
            // left
            if((wallWithLastContactPosition.x - transform.position.x) > 0)
            {
                rb.AddForce(new Vector2(-1, 1) * wallJumpStrength, ForceMode2D.Impulse);
            }
            // right
            else if((wallWithLastContactPosition.x - transform.position.x) < 0)
            {
                rb.AddForce(new Vector2(1, 1) * wallJumpStrength, ForceMode2D.Impulse);
            }
            else if((wallWithLastContactPosition.x - transform.position.x) == 0)
            {
                Debug.Log("ERROR WallJump in the Wall: " + wallWithLastContactPosition);
            }
        }
    }
    
    private void SlideFixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            startingTimeOfSlide = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            startingVelocityOfSlide = rb.velocity;
        }
        if(Input.GetKey(KeyCode.S) && IsOnGround())
        {
            slideMultiplyer = slidingLength;
            isSliding = true;

            if(Input.GetKey(KeyCode.S))
            {
            collider.size = new Vector2(colliderSize.y, colliderSize.x); 
            collider.offset = new Vector2(0, -0.4f) * playerScaling.y;
            collider.direction = CapsuleDirection2D.Horizontal;
            transform.Find("RayCastStart").transform.localPosition = new Vector3(0, -0.2f, 0) * playerScaling.y;
            }
        }
        else if(!Input.GetKey(KeyCode.S))
        {
            slideMultiplyer = 1.0f;
            isSliding = false;
        }

        if(!Input.GetKey(KeyCode.S))
        {
            collider.size = colliderSize; 
            collider.offset = Vector2.zero;
            collider.direction = CapsuleDirection2D.Vertical;
            transform.Find("RayCastStart").transform.localPosition = raycastStartlocalPosition;
        }
    }

    private void DashUpdate()
    {
        //check if dash has been canceled
        if(ForgivingFramesDashCancel > (DateTimeOffset.Now.ToUnixTimeMilliseconds() - timeSinceStartOfDash) )
        {
            if(GetCurrentDirectionTraveledX() == (Input.GetAxis("Horizontal") / Math.Abs(Input.GetAxis("Horizontal"))))
            {

            }
            // dash has been cancelled
            else
            {
                Debug.Log("Dash has been cancelled: " + (Input.GetAxis("Horizontal") / Math.Abs(Input.GetAxis("Horizontal"))));
                // dash als abgelaufen markieren
                timeInDash  = dashingLength + 0.1f;

                // momentum cancell
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        // while sliding dashing is forbidden
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isSliding)
        {   
            // startDash
            if(timeSinceLastDash > dashCooldown / 1000f)
            {   
                //setting starting time of the dash
                if(!isDashing)
                {
                    timeSinceStartOfDash = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                }

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
                Debug.Log("Dash is not ready! Time until next dash: " + ((dashCooldown / 1000) - timeSinceLastDash) + " seconds.");
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
            // ground movement
            if((IsOnGround() || IsOnSlope()) && !isSliding)
            {
                rb.AddForce(Vector2.right * horizontalInput * accelerationSpeed * Time.fixedDeltaTime);
            }
            // ai movement
            else if(!IsOnGround())
            {
                rb.AddForce(Vector2.right * horizontalInput * accelerationSpeed * Time.fixedDeltaTime * airMovementMultiplyer);
            }

            if(IsOnSlope() && !isSliding)
            {
                if(Input.GetAxis("Horizontal") != 0)
                {
                    rb.AddForce(Vector2.up * Time.fixedDeltaTime * 70 * upwardForceOnSlopes, ForceMode2D.Impulse);
                }
                
            }
            
        }

        // slow down
        if(IsOnGround() && (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) || isSliding))
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
        
        if(isSliding)
        {
            // long secondsElapsedSinceStartofSlide = (DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000) - (startingTimeOfSlide / 1000);
            double secondsElapsedSinceStartofSlide = (Convert.ToDouble(DateTimeOffset.Now.ToUnixTimeMilliseconds()) / 1000) - (Convert.ToDouble(startingTimeOfSlide) / 1000);
            
            Vector2 slideVelocityTemp = new Vector2(GetCurrentDirectionTraveledX() * 
            (-((float)(secondsElapsedSinceStartofSlide * secondsElapsedSinceStartofSlide * secondsElapsedSinceStartofSlide)/ slidingLength)
             + startingVelocityOfSlide.x), rb.velocity.y);

            if(slideVelocityTemp.x > 1)
            {
                rb.velocity = slideVelocityTemp;
            }
            else
            {
                // total stop
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else
        {
            rb.AddForce(new Vector2((float)x * timeSinceLastFrameUpdate * breakingStrength * -(GetCurrentDirectionTraveledX()), 0));
        }
        
        /*if(Math.Abs(rb.velocity.x) > 1.0f)
        {
            Debug.Log("Force: " + ((float)x * timeSinceLastFrameUpdate * breakingStrength * -(GetCurrentDirectionTraveledX()) * slideMultiplyer) + ", " + slideMultiplyer);
        }*/
        
    }

    private void JumpFixedUpdate()
    {   // Quelle: https://www.youtube.com/watch?v=7KiK0Aqtmzc zitat 1
        // code zitat: 1
        if(rb.velocity.y < 0)
        {
            float jumpVelocity = rb.velocity.y + (Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            if(!IsOnSlope() && (DateTimeOffset.Now.ToUnixTimeMilliseconds() > (timeOfStartOfFall + fallAnimationDelayOnSlopes)))
            {
                isJumping = true;
            }
        }
        // ende code zitat: 1
        else
        {
            timeOfStartOfFall = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            // code zitat: 
            if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
                if(RayCastHitDetection()[1] == null &&
                RayCastHitDetection()[2] == null &&
                RayCastHitDetection()[3] == null)
                {
                    isJumping = true;
                }
            }
            // ende code zitat: 1
            else
            {
                if(Input.GetKey(KeyCode.Space) != true)
                {
                    isJumping = false;
                }
            }
        }
    }

    // ToDo: detecton of conntact 
    // walljumps are possible
    private bool IsOnGround()
    {
        // needs maybe safety margin
        // does accept all colliders
        if(/*rb.velocity.y >= 0 &&*/ ((RayCastHitDetection()[1] != null && RayCastHitDetection()[1] != "Wall")
                || (RayCastHitDetection()[2] != null && RayCastHitDetection()[2] != "Wall")
                || (RayCastHitDetection()[3] != null && RayCastHitDetection()[3] != "Wall"))) // does a nother collider exists to stand of?
        {
            timeSinceLastDash = dashCooldown + 0.1f;
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
        // old RayCast less aproach
        /*if(collisionInfo.gameObject.tag == "Wall")
        {
            timeSinceLastContactWithWall = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            wallWithLastContactPosition = collisionInfo.gameObject.transform.position;
            Debug.Log("Collision with wall: " + collisionInfo.gameObject.tag);
        }*/
        if(collisionInfo.gameObject.tag == "LVL")
        //right
        if(RayCastHitDetection()[0] != null)
        {
            timeSinceLastContactWithWall = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            wallWithLastContactPosition = new Vector2(transform.position.x + 1, transform.position.y);
            //Debug.Log("Collision with Wall");
        }

        if(RayCastHitDetection()[4] != null)
        {
            timeSinceLastContactWithWall = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            wallWithLastContactPosition = new Vector2(transform.position.x - 1, transform.position.y);
            //Debug.Log("Collision with Wall");
        }
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

    private List<String> RayCastHitDetection()
    {
        float raycastLength = 0.7f;
        raycastLength *= transform.parent.transform.localScale.y;

        RaycastHit2D[] left = Physics2D.RaycastAll(transform.Find("RayCastStart").transform.position, Vector2.left, raycastLength);
        RaycastHit2D[] down = Physics2D.RaycastAll(transform.Find("RayCastStart").transform.position, Vector2.down, raycastLength);
        RaycastHit2D[] right = Physics2D.RaycastAll(transform.Find("RayCastStart").transform.position, Vector2.right, raycastLength);
        RaycastHit2D[] leftDown = Physics2D.RaycastAll(transform.Find("RayCastStart").transform.position, new Vector2(-1, -1), raycastLength * 1.2f);
        RaycastHit2D[] rightDown = Physics2D.RaycastAll(transform.Find("RayCastStart").transform.position, new Vector2(1, -1), raycastLength * 1.2f);

        Debug.DrawRay(transform.Find("RayCastStart").transform.position, Vector2.left * raycastLength, Color.magenta);
        Debug.DrawRay(transform.Find("RayCastStart").transform.position, Vector2.right * raycastLength, Color.magenta);
        Debug.DrawRay(transform.Find("RayCastStart").transform.position, Vector2.down * raycastLength, Color.magenta);
        Debug.DrawRay(transform.Find("RayCastStart").transform.position, new Vector2(-1, -1) * raycastLength, Color.yellow);
        Debug.DrawRay(transform.Find("RayCastStart").transform.position, new Vector2(1, -1) * raycastLength, Color.yellow);

        List<String> x = new List<String>();

        List<RaycastHit2D[]> l = new List<RaycastHit2D[]>();
        // do not change the order of the adding of the lists
        l.Add(right);
        l.Add(rightDown);
        l.Add(down);
        l.Add(leftDown);
        l.Add(left);

        
        for(int y = 0; y < 5; y++)
        {
            if(l[y].Length > 1)
            {
                // the first contact is the player collider 
                x.Add(l[y][1].collider.gameObject.tag);
            }
            else
            {
                x.Add(null);
            }
        }

        return x;
    }

    private bool IsOnSlope()
    {
        if(RayCastHitDetection()[1] == "LVL" && RayCastHitDetection()[3] != "LVL")
        {
            timeOfHitOfSlope = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            //Debug.Log("Is on Slope");
            return true;
        }
        if(RayCastHitDetection()[3] == "LVL" && RayCastHitDetection()[1] != "LVL")
        {
            timeOfHitOfSlope = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            //Debug.Log("Is on Slope");
            return true;
        }
        if(DateTimeOffset.Now.ToUnixTimeMilliseconds() < (timeOfHitOfSlope + slopeDelay))
        {
            //Debug.Log("Slope air push");
            return true;
        }
        //Debug.Log(RayCastHitDetection()[1] + ", " + RayCastHitDetection()[2] + ", " + RayCastHitDetection()[3]);
        return false;
    }
    public static bool GetIsJumping()
    {
        // if() for slope
        return isJumping;
    }

    public static bool GetIsDashing()
    {
        return isDashing;
    }

    public static bool GetIsSliding()
    {
        return isSliding;
    }

    public static float GetCurrentSpeed()
    {
        // returns values between -1 and 1 
        return Mathf.Abs(Input.GetAxis("Horizontal"));
    }
}
