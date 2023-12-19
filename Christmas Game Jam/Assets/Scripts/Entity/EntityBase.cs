using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    protected abstract float MaxHealth { get; set;  }
    
    protected abstract float CurrentHealth { get; set; }

    // Returns a bool of weather or not it could add the full health amount
    public virtual bool AddHealth(float amount)
    {
        if (CurrentHealth + amount > MaxHealth)
        {
            CurrentHealth = MaxHealth;
            return false;
        }

        CurrentHealth += amount;
        return true;
    }

    // Returns a bool of weather or not the entity is alive
    public virtual bool SubtractHealth(float amount)
    {
        if (CurrentHealth - amount <= 0)
        {
            CurrentHealth = 0;
            Die();
            return false;
        }
        
        CurrentHealth -= amount;
        return true;
    }

    public float GetMaxHealth()
    {
        return MaxHealth;
    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    protected abstract void InitializeAbstractedStats();

    protected abstract void Die();
}
