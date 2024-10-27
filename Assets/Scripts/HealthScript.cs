using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure to include the TextMeshPro namespace

public class HealthScript : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    private int currentHealth; // Current health of the player

    public TextMeshProUGUI PlayerHealthText; // Reference to the UI TextMeshPro for health display

    private void Start()
    {
        currentHealth = maxHealth; // Set current health to maximum at the start
        UpdateHealthText(); // Initialize health display
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by damage amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp health to not go below 0
        UpdateHealthText(); // Update health display
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount; // Increase current health by heal amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp health to not exceed max
        UpdateHealthText(); // Update health display
    }

    private void UpdateHealthText()
    {
        if (PlayerHealthText != null)
        {
            PlayerHealthText.text = "Health: " + currentHealth; // Update health display
        }
    }
}
