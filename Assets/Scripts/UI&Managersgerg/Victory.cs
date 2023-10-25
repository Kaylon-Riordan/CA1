using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    // Set a public boolean to track if the player has won
    public bool win = false;
    // This method is called whenever the traps collider collides with another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player collides with the door, set win to true;
        if (collision.tag == "Player")
        {
            win = true;
        }
    }
}
