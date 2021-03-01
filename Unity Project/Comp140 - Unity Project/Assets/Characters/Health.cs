using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(this.gameObject + " was hit for " + damage);
        currentHealth -= damage;
    }

    public void Heal(float heal)
    {
        Debug.Log(this.gameObject + " was healed for " + heal);
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
    }
}