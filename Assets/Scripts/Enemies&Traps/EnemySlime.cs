using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    public float speed = -4f;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    private Rigidbody2D body;
    private float flipCooldown = 1f;
    private float flipTimer = 1f;
    private float damage = 1;

    // Awake method is called when the script is loaded
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.velocity = new Vector2(speed, body.velocity.y);
        if((body.position.x <= left.position.x || body.position.x >= right.position.x) && flipTimer > flipCooldown)
        {
            Flip();
        }
        flipTimer += Time.deltaTime;
    }

     // Creates a method to turn the character around
    private void Flip()
    {
        // Rotates the character on the y axis by 180, fliping them horizontaly 
        transform.Rotate(0f, 180f, 0f);
        speed *= -1;
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
