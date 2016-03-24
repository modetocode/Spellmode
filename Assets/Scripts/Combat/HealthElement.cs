using System;
using UnityEngine;

/// <summary>
/// Health element that can be attached to an entity. 
/// </summary>
public class HealthElement {

    public event Action<float> HealthChanged;

    public float Health { get; private set; }
    public float MaxHealth { get; private set; }

    public HealthElement(float maxHealth) {
        if (maxHealth < 0) {
            throw new ArgumentOutOfRangeException("maxHealth");
        }

        this.MaxHealth = maxHealth;
        this.Health = this.MaxHealth;
    }

    /// <summary>
    /// Increases the current health for the given ammount. The new value cannot surpass the value of the max health.
    /// </summary>
    /// <param name="ammount"></param>
    public void IncreaseHealth(float ammount) {
        if (ammount < 0) {
            throw new ArgumentOutOfRangeException("ammount");
        }

        this.ChangeHealth(ammount);
    }

    /// <summary>
    /// Decreases the current health for the given ammount.
    /// </summary>
    /// <param name="ammount"></param>
    public void DecreaseHealth(float ammount) {
        if (ammount < 0) {
            throw new ArgumentOutOfRangeException("ammount");
        }

        this.ChangeHealth(-ammount);
    }

    private void ChangeHealth(float deltaHealth) {
        float previousHealth = this.Health;
        float newValue = this.Health + deltaHealth;
        this.Health = Mathf.Clamp(newValue, 0f, this.MaxHealth);
        float changeInHealth = this.Health - previousHealth;
        if (this.HealthChanged != null) {
            this.HealthChanged(changeInHealth);
        }
    }
}
