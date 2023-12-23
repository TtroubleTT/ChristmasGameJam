using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBase : EntityBase
{
    [Header("Player Stats")] 
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    
    protected override float MaxHealth { get; set;  }
    
    protected override float CurrentHealth { get; set; }

    private Image _barImage;

    protected override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected override void InitializeAbstractedStats()
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }

    private void InitializeHealthBar()
    {
        _barImage = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
        _barImage.fillAmount = CurrentHealth / MaxHealth;
    }

    private void Awake()
    {
        InitializeAbstractedStats();
        InitializeHealthBar();
    }

    public override bool AddHealth(float amount)
    {
        bool addedHealth = base.AddHealth(amount);
        _barImage.fillAmount = CurrentHealth / MaxHealth;
        return addedHealth;
    }
    
    // Returns a bool of weather or not the entity is alive
    public override bool SubtractHealth(float amount)
    {
        bool subtractedHealth = base.SubtractHealth(amount);
        _barImage.fillAmount = CurrentHealth / MaxHealth;
        return subtractedHealth;
    }
}
