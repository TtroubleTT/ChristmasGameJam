using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    protected override float MaxHealth { get; set; }
    
    protected override float CurrentHealth { get; set; }

    protected override float DistanceToKeepFromPlayer { get; set; }

    protected override float MovementSpeed { get; set; }

    [Header("Enemy Stats")]
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float currentHealth = 50f;
    [SerializeField] private float distanceToKeepFromPlayer = 5f;
    [SerializeField] private float movementSpeed = 20f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float damage = 20f;
    private float _lastAttackTime;

    protected override void InitializeAbstractedStats()
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        DistanceToKeepFromPlayer = distanceToKeepFromPlayer;
        MovementSpeed = movementSpeed;
    }
    
    private void Start()
    {
        InitializeAbstractedStats();
    }
    
    private void Update()
    {
        LookAtPlayer();
        MoveTowardsPlayer();
    }
}
