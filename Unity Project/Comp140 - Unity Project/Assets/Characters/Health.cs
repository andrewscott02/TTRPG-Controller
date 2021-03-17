using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(this.gameObject + " was hit for " + damage);
        currentHealth -= damage;

        if (CheckDeath())
            Die();
    }

    public void Heal(int heal)
    {
        Debug.Log(this.gameObject + " was healed for " + heal);
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
    }

    private bool CheckDeath()
    {
        return currentHealth <= 0;
    }

    private void Die()
    {
        //delete target areas
        Invoke("Destroy", 0.2f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}