using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Set a variable for the traps dmage
    private float damage = 1;

    // This method is called whenever the traps collider collides with another collider
    // This method is based on code in this video (Pandemonium (2021). Unity 2D Platformer for Complete Beginners - #7 HEALTH SYSTEM. YouTube. Available at: https://www.youtube.com/watch?v=yxzg8jswZ8A&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=7 [Accessed 20 Oct. 2023].)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the object the trap collided with is tagged as a character and the character isnt currently invunerable, reduce the character's health by the traps damage
        if (collision.tag == "Character" && collision.GetComponent<Health>().isInvulnerable()==false)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}