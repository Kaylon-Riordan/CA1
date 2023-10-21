using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Set variables for max health, current health, invulnerability period, spreite renderer and an animator
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private float invulnerability = 1f;
    public float currentHealth;
    private Animator anim;
    private SpriteRenderer spriRend;

    // Awake method is called when the script is loaded
    void Awake()
    {
        // Set the animator and renderer to the player's animator and sprite renderer, and set current health to start at max health
        anim = GetComponent<Animator>();
        spriRend = GetComponent<SpriteRenderer>();;
        currentHealth = maxHealth;
    }

    // Update method was used to test the damage emthod before traps were added
    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.F))
    //     {
    //         TakeDamage(1);
    //     }
    // }

    // Creates a public method to take damage that will be called in other scripts
    // This method is based on code in this video (Pandemonium (2021). Unity 2D Platformer for Complete Beginners - #7 HEALTH SYSTEM. YouTube. Available at: https://www.youtube.com/watch?v=yxzg8jswZ8A&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=7 [Accessed 20 Oct. 2023].)
    public void TakeDamage(float damage)
    {
        // Set current health to a clamp, which has a minimum value of 0 and a maximum value of the max health varialbe, and take away however much damage was inflicted from the current health
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        // Trigger the hurt animation
        anim.SetTrigger("hurt");

        // Calls the Invulnerable method when damage is taken then invokes the Vulnerable method after a number of seconds determined by the invulnerability variable, giving the player iFrames after damage
        Invulnerable();
        Invoke ("Vulnerable", invulnerability);
    }

    // The next 2 methods are based on code in this video (Pandemonium (2021). Unity 2D Platformer for Complete Beginners - #8 IFRAMES. YouTube. Available at: https://www.youtube.com/watch?v=YSzmCf_L2cE&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=9 [Accessed 21 Oct. 2023].)
    // This method thurns off collison for items on the player and enemy layers, making the player immune to damage, also tints player red to indicate they have taken damage
    public void Invulnerable()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        spriRend.color = Color.red;
    }

    // This method thurns on collison for items on the player and enemy layers, making the player vulnerable to damage, also sets player back to default colours
    public void Vulnerable()
    {
        Physics2D.IgnoreLayerCollision(9, 10, false);
        spriRend.color = Color.white;
    }
}