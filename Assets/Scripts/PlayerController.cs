using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float movementSpeed = 10f;
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
            playerRB.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
        }
    }

    private void JumpUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
