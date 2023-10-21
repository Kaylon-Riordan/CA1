using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// This script is based on code in this video (Pandemonium (2021). Unity 2D Platformer for Complete Beginners - #7 HEALTH SYSTEM. YouTube. Available at: https://www.youtube.com/watch?v=yxzg8jswZ8A&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=7 [Accessed 20 Oct. 2023].)
public class HealthBar : MonoBehaviour
{
    // Sets up 3 serialize fields which can link the 2 healthbar images and the health script to this script
    [SerializeField] private Health health;
    [SerializeField] private Image full;
    [SerializeField] private Image depleted;

    // Start is called before the first frame update
    void Start()
    {
        // Sets fill amount to 1/5 of the health which will show that many hearts up to 5, will not change so empty hearts can be seen when full eharts are lost
        depleted.fillAmount = health.currentHealth * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Sets fill amount to 1/5 of the health which will show that many hearts up to 5, will update as health changes
        full.fillAmount = health.currentHealth * 0.2f;
    }
}