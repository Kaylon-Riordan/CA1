using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Declare variables for stats like move speed, jump height and player height
    public float speed = 6f;
    public float jumpHeight = 10f;
    public float height;
    public float gravityScale = 2f;
    // Declares a Layer which can be used later to check if the player is on the ground
    public LayerMask layerGround; 
    // Declaures a vector which will be used later to tell iof the players feet are pointing up or down
    public Vector2 feet;
    // Declare variables which will be used to link collider, animator and rigid body between script and engine
    private CapsuleCollider2D coll;
    private Rigidbody2D body;
    private Animator anim;
    // Declare booleans which will be used to check if player is crouching or sliding, what direction they are facing nad wether they can swap gravity
    private bool isCrouching = false;
    public bool isSliding = false;
    public bool faceRight = true;
    public bool canGrav = true;

    // Awake method is called when the script is loaded
    void Awake()
    {
        // Links the variables in script to the matching components in engine
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
        body.gravityScale = gravityScale;
        // Sets the direction feet are facing to down by default
        feet = Vector2.down;
        // Sets the height variable to the height of the players capsule collider
        height = coll.size.y;
    }

    // Fixed update is used for movement so that the players speed isn't affected by framerate
    void FixedUpdate()
    {
        // Creates a variable that stores a float based on the players input on the horizontal axis, raw means there is no smoothing so is better fordigital input like keyboard
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        // Changes the rigid bodyie's velocity to be the detected horizontal input multiplied by the player's speed allowing it to move left and right
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        // Declare horizontal input again for normal update method
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        // Sets it so player can swap gravity as long as they have touched ground since last using it
        if(isGrounded())
        {
            canGrav = true;
        }
        // Sets 4 booleans which can be used by the animator to trigger transitions between animations for running, jumping, crouching and sliding
        // Code for animator based on this video (Pandemonium (2020). Unity 2D Platformer for Complete Beginners - #2 ANIMATION. YouTube. Available at: https://www.youtube.com/watch?v=Gf8LOFNnils&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=2 [Accessed 14 Oct. 2023].)
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("crouch", isCrouching);
        anim.SetBool("slide", isSliding);

        // If the space key is pressed and the player is grounded, call the jump method
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Jump();
        }
        // If the space key is released and the player has vertical velocity, run hop method
        if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y != 0)
        {
            Hop();
        }
        // If the player moves left while facing right, or moves right while facing left, will call the flip method
        if((horizontalInput>0 && !faceRight)||(horizontalInput<0 && faceRight))
        {
            Flip();
        }

        // If the player is on the ground and has the s key down while still then run the crouch method
        if(Input.GetKey(KeyCode.S) && isGrounded() && horizontalInput == 0)
        {
            Crouch();
        }

        // If the player is on the ground and has the s key down while moving then run the crouch method
        if(Input.GetKey(KeyCode.S) && isGrounded() && horizontalInput != 0)
        {
            Slide();
        }

        // If the s key is lifted up then run the stand method
        if(Input.GetKeyUp(KeyCode.S))
        {
            Stand();
        }

        // If g is pressed, call the method to flip gravity
        if(Input.GetKeyDown(KeyCode.G) && canGrav)
        {
            Grav();
        }
    }

    // Creates a method that makes the player jump
    public void Jump()
    {
        // Changes the velocity of the players rigid body along the vertical axis
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        // Send the animator the jump trigger when player jumps
        anim.SetTrigger("jump");
        // Play the jump sound from the audio manager
        AudioManager.instance.PlayPlayerJumpSound();
    }

    //Creates a method to vary jump height based on button press length
    // This method is based on code in this video (Pandemonium (2022). Unity 2D Platformer for Complete Beginners - #12 ADVANCED JUMPING. YouTube. Available at: https://www.youtube.com/watch?v=oFO4pgrQPOI&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=12 [Accessed 25 Oct. 2023].)
    private void Hop()
    {
        // When space is released, half vertical velocity, cutting jump short
        body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2f);
    }

    // Creates a method to turn the character around
    private void Flip()
    {
        // Rotates the character on the y axis by 180, fliping them horizontaly 
        transform.Rotate(0f, 180f, 0f);
        // Changes a boolean to keep track of waht direction the character is facing
        faceRight = !faceRight;
    }

    // Creates a method to crouch
    private void Crouch()
    {

        // Changes the player colliders vertical sclae to better match the croutching sprite
        coll.size = new Vector3(coll.size.x, 0.8f * height);
        // Sets speed to 0 so player cant move while crouching
        speed = 0f;
        // Changes variables which are used by the animator
        isCrouching = true;
        isSliding = false;
    }

    // Creates a method to slide
    private void Slide()
    {
        // Changes the player colliders vertical sclae to better match the sliding sprite
        coll.size = new Vector3(coll.size.x, 0.8f * height);
        // Changes variables which are used by the animator
        isSliding = true;
    }

    // Creates a method to stand back up after crouching
    private void Stand()
    {
        // Sets the players collider back to origional height
        coll.size = new Vector3(coll.size.x, height);
        // Sets speed back to 10 so player can move after crouching
        speed = 10f;
        // Changes variables which are used by the animator
        isCrouching = false;
        isSliding = false;
    }

    // This method swaps the direction of the players gravity
    private void Grav()
    {
        // Swaps gravity direction on the rigid body
        body.gravityScale *= -1;
        // Swaps the direction the player jumps in to opose its gravity
        jumpHeight *= -1;
        // Rotates the character on the x axis by 180, fliping them vertically
        transform.Rotate(180f, 0f, 0f);
        // Sets it so player cant use gravity reverse after using it, until ground is touched
        canGrav = false;
        // SPlays grav sound effect through audio manager instance
        AudioManager.instance.PlayGravSound();
        // Send the animator the jump trigger when player swaps gravity
        anim.SetTrigger("jump");
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
    public bool isGrounded()
    {
        // Casts a box ray to check if there is a ground object at the players feet and returns result
        RaycastHit2D raycastHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, feet, 0.1f, layerGround);
        return raycastHit.collider != null;
    }
}