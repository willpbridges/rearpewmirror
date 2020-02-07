using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;

    private float maxHealth;
    private float currentHealth;

    // Update is called once per frame
    void Update()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void SetMaxHealth(float max) {
        maxHealth = max;
    }

    public void UpdateCurrentHealth(float current) {
        currentHealth = current;
    }
}
