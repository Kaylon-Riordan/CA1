using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Set variables for max health, invunerable status, current health, iframe length, spreite renderer and an animator
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private float iFrameLength = 1f;
    private bool invulnerable = false;
    public float currentHealth;
    private Animator anim;
    private SpriteRenderer spriRend;

    // Awake method is called when the script is loaded
    void Awake()
    {
        // Set the animator and renderer to the character's animator and sprite renderer, and set current health to start at max health
        anim = GetComponent<Animator>();
        spriRend = GetComponent<SpriteRenderer>();;
        currentHealth = maxHealth;
    }

    // Update method was used to test the damage method before traps were added
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

        // Calls the Invulnerable method when damage is taken, giving the character iFrames after damage
        if (currentHealth > 0f)
        Invulnerable();
        // If the character still has health, invokes the Vulnerable method after a number of seconds determined by the iFrameLegth variable
        {
            Invoke ("Vulnerable", iFrameLength);
        }
        // If the character has lost all health, runs the Die method
        else 
        {
            Die();
        }
        
    }

    // The next 2 methods are based on code in this video (Pandemonium (2021). Unity 2D Platformer for Complete Beginners - #8 IFRAMES. YouTube. Available at: https://www.youtube.com/watch?v=YSzmCf_L2cE&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=9 [Accessed 21 Oct. 2023].)
    // This method thurns off collison for items on the character and enemy layers, making the character immune to damage, also tints character red to indicate they have taken damage
    public void Invulnerable()
    {
        invulnerable = true;
        spriRend.color = Color.red;
    }

    // This method turns on collison for items on the character and enemy layers, making the character vulnerable to damage, also sets character back to default colours
    public void Vulnerable()
    {
        invulnerable = false;
        spriRend.color = Color.white;
    }

    // This method destroys a character when it dies
    public void Die()
    {
        Destroy(gameObject);
    }

    // This method is used by other scripts to check weather the player is currently invunerable
    public bool isInvulnerable()
    {
        return invulnerable;
    }
}
