using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Set variables for max health, current health and an animator
    [SerializeField] private float maxHealth = 3;
    public float currentHealth;
    private Animator anim;

    // Awake method is called when the script is loaded
    void Awake()
    {
        // Set the animator to the player's animator, and set current health to start at max health
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Creates a public method to take damage that will be called in other scripts
    // This method is based on code in this video (Pandemonium (2021). Unity 2D Platformer for Complete Beginners - #7 HEALTH SYSTEM. YouTube. Available at: https://www.youtube.com/watch?v=yxzg8jswZ8A&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=7 [Accessed 20 Oct. 2023].)
    public void TakeDamage(float damage)
    {
        // Set current health to a clamp, which has a minimum value of 0 and a maximum value of the max health varialbe, and take away however much damage was inflicted from the current health
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        // Trigger the hurt animation
        anim.SetTrigger("hurt");
    }
}