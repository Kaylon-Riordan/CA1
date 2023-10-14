using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Declare variables for stats like move speed, jump height and player height
    public float speed = 10f;
    public float jumpHeight = 6f;
    public float height;
    // Declares a Layer which can be used later to check if the player is on the ground
    public LayerMask layerGround; 
    // Declaures a vector which will be used later to tell iof the players feet are pointing up or down
    public Vector2 feet;
    // Declare a vector which is used for fliping our player character in different methods
    Vector3 currentScale;
    // Declare variables which will be used to link collider, animator and rigid body between script and engine
    private CapsuleCollider2D coll;
    private Rigidbody2D body;
    private Animator anim;
    // Declare booleans which will be used to check if player is crouching or sliding and what direction they are facing
    private bool isCrouching = false;
    private bool isSliding = false;
    private bool faceRight = true;

    // Awake method is called when the script is loaded
    void Awake()
    {
        // Links the variables in script to the matching components in engine
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
        // sets the direction feet are facing to down by default
        feet = Vector2.down;

        height = coll.size.y;
        // sets the currentScale variable to match the scale of the player in engine
        currentScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Creates a variable that stores a float based on the players input on the horizontal axis, raw means there is no smoothing so is better fordigital input like keyboard
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        // Changes the rigid bodyie's velocity to be the detected horizontal input multiplied by the player's speed allowing it to move left and right
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        // Sets 3 booleans which can be used by the animator to trigger transitions between animations for running and jumping
        // Code for animator based on this video (Pandemonium (2020). Unity 2D Platformer for Complete Beginners - #2 ANIMATION. YouTube. Available at: https://www.youtube.com/watch?v=Gf8LOFNnils&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=2 [Accessed 14 Oct. 2023].)
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("crouch", isCrouching);
        anim.SetBool("slide", isSliding);

        // If the space key is pressed and the player is grounded, call the jump method
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Jump();
            anim.SetTrigger("jump");
        }

        // If the player moves left while facing right, or moves right while facing left, will call the flip method
        if((horizontalInput>0 && !faceRight)||(horizontalInput<0 && faceRight))
        {
            Flip();
        }

        if(Input.GetKey(KeyCode.S) && isGrounded() && horizontalInput == 0)
        {
            coll.size = new Vector3(coll.size.x, 0.8f * height);
            speed = 0f;
            isCrouching = true;
        }
        else if(Input.GetKey(KeyCode.S) && isGrounded() && horizontalInput != 0)
        {
            coll.size = new Vector3(coll.size.x, 0.8f * height);
            isCrouching = false;
            isSliding = true;
        }

        if(Input.GetKeyUp(KeyCode.S))
        {
            coll.size = new Vector3(coll.size.x, height);
            speed = 10f;
            isCrouching = false;
            isSliding = false;
        }

        // If g is pressed, call the method to flip gravity
        if(Input.GetKeyDown(KeyCode.G))
        {
            Grav();
        }
    }

    // Creates a method that makes the player jump
    private void Jump()
    {
        // Changes the velocity of the players rigid body along the vertical axis
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
    }

    // Creates a method to turn the character around
    private void Flip()
    {
        // Multiplies the characters x axis sclae by -1, fliping the character horizontaly
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        // Changes a boolean to keep track of waht direction the character is facing
        faceRight = !faceRight;
    }

    // This method swaps the direction of the players gravity
    private void Grav()
    {
        // Swaps gravity direction on the rigid body
        body.gravityScale *= -1;
        // Swaps the direction the player jumps in to opose its gravity
        jumpHeight *= -1;
        // Multiplies the characters y axis sclae by -1, fliping the character verticaly
        currentScale.y *= -1;
        gameObject.transform.localScale = currentScale;
        // Changes the direction the characters feet point in to match the sprite so ground can accurately be checked
        if(feet == Vector2.down)
        {
            feet = Vector2.up;
        }
        else
        {
            feet = Vector2.down;
        }
    }

    // Uses a method to check if player is on the ground
    // Code for this method is based off this video (Pandemonium (2021). Unity 2D Platformer for Complete Beginners - #3 WALL JUMPING. YouTube. Available at: https://www.youtube.com/watch?v=_UBpkdKlJzE&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=3 [Accessed 14 Oct. 2023].‌)
    private bool isGrounded()
    {
        // Casts a box ray to check if there is a ground object at the players feet and returns result
        RaycastHit2D raycastHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, feet, 0.1f, layerGround);
        return raycastHit.collider != null;
    }
}