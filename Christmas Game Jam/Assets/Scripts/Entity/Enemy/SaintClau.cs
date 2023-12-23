using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaintClau : EnemyBase
{
    protected override float MaxHealth { get; set; }
    
    protected override float CurrentHealth { get; set; }

    protected override float DistanceToKeepFromPlayer { get; set; }

    protected override float MovementSpeed { get; set; }

    protected override CharacterController CharacterController { get; set; }
    
    [Header("Enemy Stats")]
    [SerializeField] private float maxHealth = 200f;
    [SerializeField] private float currentHealth = 200f;
    [SerializeField] private float distanceToKeepFromPlayer = 5f;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float meleeDamage = 20f;
    [SerializeField] private float shotRange = 120f;
    [SerializeField] private float shotCooldown = 3f;
    private float _lastAttackTime;
    
    [Header("Projectile Stats")] 
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float range = 70f;

    [Header("References")] 
    [SerializeField] private CharacterController controller;
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private Animator animator;
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

    private bool IsInMeleeRange()
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
        
        if (Time.time - _lastAttackTime > attackCooldown && IsInMeleeRange())
        {
            _lastAttackTime = Time.time;
            Attack();
        }
    }

    private void Attack()
    {
        animator.Play("Bip001|SLAP");
        _playerBase.SubtractHealth(meleeDamage);
    }
}
