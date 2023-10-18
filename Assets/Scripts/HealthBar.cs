using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image full;
    [SerializeField] private Image depleted;

    // Start is called before the first frame update
    void Start()
    {
        depleted.fillAmount = playerHealth.currentHealth * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        full.fillAmount = playerHealth.currentHealth * 0.2f;
    }
}