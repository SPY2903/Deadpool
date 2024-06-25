using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    private int value;

    public void Initialize(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
        value = health;
    }
    public void SetHealth(int health)
    {
        value = health;
        //healthBar.value = health;
    }
    public void UpgradeHealth(int maxHealth, int currentHealth)
    {
        //if(currentHealth != healthBar.maxValue)
        //{
        //    value = currentHealth;
        //}
        //else
        //{
        //    value = maxHealth;
        //}
        if (healthBar.value == healthBar.maxValue)
        {
            value = maxHealth;
        }
        else
        {
            value = currentHealth;
        }
        healthBar.maxValue = maxHealth;
        healthBar.value = 0;
    }

    private void Update()
    {
        healthBar.value = Mathf.MoveTowards(healthBar.value, value, Time.deltaTime * 50);
    }

}
