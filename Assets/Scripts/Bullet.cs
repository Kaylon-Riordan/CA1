using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 12f;
    private bool hit;
    private CircleCollider2D coll;
    private Animator anim;
    private Rigidbody2D body;
    private PlayerMovement playerMovement;

    // Awake method is called when the script is loaded
    void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = transform.right * speed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        anim.SetTrigger("Hit");
    }
}