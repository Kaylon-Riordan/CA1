using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 12f;
    private bool hit;
    private float decay;
    [SerializeField] private CircleCollider2D coll;
    private Animator anim;
    private Rigidbody2D body;

    // Awake method is called when the script is loaded
    void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        body.velocity = transform.right * speed;
        coll.enabled = true;
    }

    private void Update()
    {
        if (hit) return;

        decay += Time.deltaTime;
        if (decay > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        anim.SetTrigger("Hit");
        body.velocity = Vector2.zero;
        transform.Rotate(180f, 0f, 0f);
    }
}