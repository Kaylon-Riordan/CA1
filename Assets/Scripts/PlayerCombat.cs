using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement playerMovement;
    private float shootCooldown = 3f;
    private float shootTimer = 10f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gun;

    // Awake method is called when the script is loaded
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && shootTimer>shootCooldown && playerMovement.isGrounded() && playerMovement.isSliding == false)
        {
            Shoot();
        }
        shootTimer += Time.deltaTime;
    }

    private void Shoot()
    {
        shootTimer = 0f;
        anim.SetTrigger("shoot");
        Instantiate(bulletPrefab, gun.position, transform.rotation);
    }
}
