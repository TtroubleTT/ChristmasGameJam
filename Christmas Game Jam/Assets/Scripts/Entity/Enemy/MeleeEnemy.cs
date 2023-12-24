using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    protected override float MaxHealth { get; set; }
    
    protected override float CurrentHealth { get; set; }

    protected override float DistanceToKeepFromPlayer { get; set; }

    protected override float MovementSpeed { get; set; }

    protected override CharacterController CharacterController { get; set; }

    [Header("Enemy Stats")]
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float currentHealth = 50f;
    [SerializeField] private float distanceToKeepFromPlayer = 5f;
    [SerializeField] private float movementSpeed = 20f;
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float damage = 20f;
    private float _lastAttackTime;

    [Header("References")] 
    [SerializeField] private CharacterController controller;
    private PlayerBase _playerBase;

    protected override void InitializeAbstractedStats()
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        DistanceToKeepFromPlayer = distanceToKeepFromPlayer;
        MovementSpeed = movementSpeed;
        CharacterController = controller;
    }
    
    private void Start()
    {
        InitializeAbstractedStats();
        _playerBase = Player.GetComponent<PlayerBase>();
    }
    
    private void Update()
    {
        Gravity();
        LookAtPlayer();
        MoveTowardsPlayer();
        CheckAttack();
    }

    private bool IsInRange()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);

        if (distance > attackRange)
            return false;

        return true;
    }

    private void CheckAttack()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);

        if (distance >= attackRange)
            return;
        
        if (Time.time - _lastAttackTime > attackCooldown && IsInRange())
        {
            _lastAttackTime = Time.time;
            Attack();
        }
    }

    private void Attack()
    {
        _playerBase.SubtractHealth(damage);
    }
}
