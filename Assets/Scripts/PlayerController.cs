using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float movementSpeed = 10f;
    private bool isGrounded = true;
    Rigidbody2D playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
        DashUpdate();
        JumpUpdate();
        Debug.Log("Velocity: " + playerRB.velocity);
    }



    private void MoveUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * Time.deltaTime * movementSpeed * horizontalInput);
    }

    private void DashUpdate()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(playerRB.velocity.x > 0)
            {
                playerRB.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
            }else{
                playerRB.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
            }

        }
    }

    private void JumpUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Ground"))
        {
        isGrounded = true;
        }
    }*/

   /* void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }*/
}
