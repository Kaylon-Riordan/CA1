using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Declare variables which will be used to link  animator, player movement script, spawn point for bullets and bullet prefab between script and engine
    private Animator anim;
    private PlayerMovement playerMovement;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gun;
    // Declare variables to store time since last shot and the amount of time between shots
    private float shootCooldown = 1f;
    private float shootTimer = 10f;

    // Awake method is called when the script is loaded
    void Awake()
    {
        // Links the variables in script to the matching components in engine
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    // This method is based off the following video (Pandemonium (2021). Unity 2D Platformer for Complete Beginners - #4 SHOOTING. YouTube. Available at: https://www.youtube.com/watch?v=PUpC44Q64zY&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=4 [Accessed 17 Oct. 2023].)
    void Update()
    {
        // If left click is pressed, more time has passed since shooting than the cooldown, and the player is grounded, and th player isnt sliding, then shoot
        if(Input.GetMouseButton(0) && shootTimer>shootCooldown && playerMovement.isGrounded() && playerMovement.isSliding == false)
        {
            Shoot();
        }
        // Increase the timer variable by delta time, which is a rough calculation of every second
        shootTimer += Time.deltaTime;
    }

    // This method is based off the following video (Games, R. (2021). Unity: 2D Shooting. YouTube. Available at: https://www.youtube.com/watch?v=vkKulG71Yzo [Accessed 17 Oct. 2023].)
    private void Shoot()
    {
        // Reset the timer to 0
        shootTimer = 0f;
        // Play the shoot animation
        anim.SetTrigger("shoot");
        // Create a copy of the bullet prefab
        Instantiate(bulletPrefab, gun.position, transform.rotation);
    }
}