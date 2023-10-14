using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Declare variables
    public float speed = 10f;
    public float jumpHeight = 6f;
    public float crouchHeight = 0.5f;

    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private bool isCrouching = false;
    private bool faceRight = true;

    // Awake method is called when the script is loaded
    void Awake()
    {
        // Links the variables in script to the matching components in engine
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Creates a variable that stores a float based on the players input on the horizontal axis, raw means there is no smoothing so is better fordigital input like keyboard
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);

        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
            anim.SetTrigger("jump");
        }

        // If the player moves left while facing right, or moves right while facing left, will call the flip method
        if((horizontalInput>0 && !faceRight)||(horizontalInput<0 && faceRight))
        {
            Flip();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow) && grounded)
        {
            if(!isCrouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
                isCrouching = true;
            }
            else if(isCrouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
                isCrouching = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            Grav();
        }
    }

    //Creates a method that makes the player jump
    private void Jump()
    {
        //changes the velocity of the players rigid body along the vertical axis
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        faceRight = !faceRight;
    }

    private void Grav()
    {
        body.gravityScale *= -1;
        jumpHeight *= -1;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.y *= -1;
        gameObject.transform.localScale = currentScale;
    }
}