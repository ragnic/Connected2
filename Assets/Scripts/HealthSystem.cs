using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100;
    [HideInInspector]
    public float currentHealth;
    public Slider healthBar;

    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth / maxHealth;
    }

    public void OnDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth / maxHealth;
    }
}
