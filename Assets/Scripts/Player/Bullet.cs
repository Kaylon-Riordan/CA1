using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Declare a float which is used to decide the bullets speed
    private float speed = 12f;
    //Declare a float which is used to measure how long the bullet has existed so it can be deleted after a certain time
    private float decay;
    // Declare variables which will be used to link collider, animator and rigid body between script and engine
    private CircleCollider2D coll;
    private Animator anim;
    private Rigidbody2D body;

    // Awake method is called when the script is loaded
    void Awake()
    {
        // Links the variables in script to the matching components in engine
        coll = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Sets the velocity of the bullet to the speed value
        body.velocity = transform.right * speed;
    }

    // This method is based off the following video (Pandemonium (2021). Unity 2D Platformer for Complete Beginners - #4 SHOOTING. YouTube. Available at: https://www.youtube.com/watch?v=PUpC44Q64zY&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=4 [Accessed 17 Oct. 2023].)
    // Update is called once per frame
    private void Update()
    {
        // Increase the decay variable by delta time, which is a rough calculation of every second
        decay += Time.deltaTime;
        // If 5 seconds pass, destroy the bullet
        if (decay > 5) Destroy(gameObject);
    }

    // This method is called whenever the bullets collider collides with another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set the hit animaton trigger when the bullet hits something
        anim.SetTrigger("hit");
        // Stop the bullets velocity when it hits something
        body.velocity = Vector2.zero;
        // Destroy the bullet half a second after it hits something
        Invoke ("Destroy", 0.5f);
    }

    // This method is called to destroy the bullet when it hits something
    public void Destroy()
    {
        Destroy(gameObject);
    }
}