using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        animator.SetBool("isJumping", PlayerMovement.GetIsJumping());
        animator.SetBool("isDashing", PlayerMovement.GetIsDashing());
        animator.SetBool("isSliding", PlayerMovement.GetIsSliding());
        animator.SetFloat("Speed", PlayerMovement.GetCurrentSpeed());
        AnimDirectionFixedUpdate();
    }

    //Zitat, Quelle: https://www.youtube.com/watch?v=4qE8cuHI93c
    public bool facingRight = true; //Default direction of sprite
    public bool facingAwayFromWall = false;
    //Dtermining which direction player is facing
    void AnimDirectionFixedUpdate()
    {
        //TODO: Funktioniert noch nicht richtig
        // if at wall change locking direction away from wall
        /*if(PlayerMovement.GetIsAtWall())
        {
            if((transform.parent.localScale.x / Mathf.Abs(transform.parent.localScale.x)) == PlayerMovement.GetIsWallRight())
            {
                Debug.Log("Change");
                Vector3 lookingDirection = transform.parent.localScale;
                lookingDirection.x *= PlayerMovement.GetIsWallRight();
                transform.parent.localScale = lookingDirection;
                facingRight = !facingRight;
            }
        }
        else
        {*/
        float h = Input.GetAxis("Horizontal");
        if(h > 0 && !facingRight)
            Flip();
        else if(h < 0 && facingRight)
            Flip();
        //}
     }

    //Flipping animation sprite into correct direction
    void Flip ()
    {   
        facingRight = !facingRight;
        // changes the x skale
        Vector3 theScale = transform.parent.localScale;
        theScale.x *= -1;
        transform.parent.localScale = theScale;
    }
    // Zitat ende
}
