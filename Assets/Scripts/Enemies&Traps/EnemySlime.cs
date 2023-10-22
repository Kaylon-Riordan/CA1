using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    // Set 2 serialize fields so the left and right movement boundaries can be placed in unity
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    // Create a referance for the rigid body
    private Rigidbody2D body;
    // Set variables for speed and damage
    public float speed = -4f;
    private float damage = 1;
    // Sets variables for a flip tiemr and cooldown, so the enemy dosnt constantly flip on spot as soon as it leaves boundaries
    private float flipCooldown = 1f;
    private float flipTimer = 1f;

    // Awake method is called when the script is loaded
    void Awake()
    {
        // Link body varaible to the slimes rigid body
        body = GetComponent<Rigidbody2D>();
    }

    // Fixed Update is called at a fixed rate so methods inside it aren't affected by framerate
    void FixedUpdate()
    {
        // Set horizontal velocity to equal the speed variable
        body.velocity = new Vector2(speed, body.velocity.y);
        // If the slime is not between the left and right boundary points, then flip
        if((body.position.x <= left.position.x || body.position.x >= right.position.x) && flipTimer > flipCooldown)
        {
            Flip();
        }
        // Increase the flip timer every second using deltatime
        flipTimer += Time.deltaTime;
    }

     // Creates a method to turn the character around
    private void Flip()
    {
        // Rotates the character on the y axis by 180, fliping them horizontaly 
        transform.Rotate(0f, 180f, 0f);
        // Multiplies speed by -1 to swap its direction
        speed *= -1;
        // Resets the flip timer to 0
        flipTimer = 0;
    }

    // This method is called whenever the slime's collider collides with another collider
    // This method is based on code in this video (Pandemonium (2021). Unity 2D Platformer for Complete Beginners - #7 HEALTH SYSTEM. YouTube. Available at: https://www.youtube.com/watch?v=yxzg8jswZ8A&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=7 [Accessed 20 Oct. 2023].)
    private void OnTriggerStay2D(Collider2D collision)
    {
        // If the object the trap collided with is tagged as a player and the player isnt currently invunerable, reduce the play's health by the traps damage
        if (collision.tag == "Player" && collision.GetComponent<Health>().isInvulnerable()==false)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
