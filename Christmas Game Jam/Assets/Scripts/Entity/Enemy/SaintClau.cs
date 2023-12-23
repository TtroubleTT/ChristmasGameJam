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
    [SerializeField] private float maxFirstFormHealth = 500f;
    [SerializeField] private float maxSecondFormHealth = 500f;
    [SerializeField] private float currentHealth = 500f;
    [SerializeField] private float distanceToKeepFromPlayerFirstForm = 20f;
    [SerializeField] private float distanceToKeepFromPlayerSecondForm = 5f;
    [SerializeField] private float movementSpeedFirstForm = 10f;
    [SerializeField] private float movementSpeedSecondForm = 15f;
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float meleeDamage = 20f;
    [SerializeField] private float shotRange = 120f;
    [SerializeField] private float shotCooldown = 0.5f;
    private float _lastAttackTime;
    private float _lastShotTime;
    private bool _isInSecondForm;
    private float _transformTime;
    
    [Header("Projectile Stats")] 
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float range = 130f;

    [Header("References")] 
    [SerializeField] private CharacterController controller;
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private Transform shootTransformation;
    [SerializeField] private GameObject projectilePrefab;
    private PlayerBase _playerBase;

    private readonly Dictionary<ShootingEnemy.Stats, float> _projectileStats = new();
    
    protected override void InitializeAbstractedStats()
    {
        MaxHealth = maxFirstFormHealth;
        CurrentHealth = currentHealth;
        DistanceToKeepFromPlayer = distanceToKeepFromPlayerFirstForm;
        MovementSpeed = movementSpeedFirstForm;
        CharacterController = controller;
    }
    
    public override bool SubtractHealth(float amount)
    {
        if (_isInSecondForm && Time.time - _transformTime < 9f)
            return false;
        
        bool subtractedHealth = base.SubtractHealth(amount);
        return subtractedHealth;
    }

    protected override void Die()
    {
        if (_isInSecondForm)
            Destroy(gameObject);
        else
        {
            _isInSecondForm = true;
            _transformTime = Time.time;
            MaxHealth = maxSecondFormHealth;
            CurrentHealth = maxSecondFormHealth;
            DistanceToKeepFromPlayer = distanceToKeepFromPlayerSecondForm;
            MovementSpeed = movementSpeedSecondForm;
            animationManager.PlaySantaAnimation(AnimationManager.SantaAnimationType.Morphing);
        }
    }
    
    private void InitializeStats()
    {
        _projectileStats.Add(ShootingEnemy.Stats.Damage, damage);
        _projectileStats.Add(ShootingEnemy.Stats.Speed, speed);
        _projectileStats.Add(ShootingEnemy.Stats.Range, range);
    }
    
    private void Start()
    {
        InitializeStats();
        InitializeAbstractedStats();
        _playerBase = Player.GetComponent<PlayerBase>();
        animationManager.PlaySantaAnimation(AnimationManager.SantaAnimationType.Beam);
    }
    
    private void Update()
    {
        Gravity();
        LookAtPlayer();

        if (!_isInSecondForm || Time.time - _transformTime > 9f)
            MoveTowardsPlayer();
        
        if (!_isInSecondForm)
            CheckShoot();
        else
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
        if (Time.time - _transformTime < 9f)
            return;
        
        if (Time.time - _lastAttackTime > attackCooldown && IsInMeleeRange())
        {
            _lastAttackTime = Time.time;
            Attack();
        }
    }

    private void Attack()
    {
        _playerBase.SubtractHealth(meleeDamage);
    }
    
    // Checks if the distance between player and enemy is within the range they are allowed to fire
    private bool IsInShotRange()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);

        if (distance <= shotRange)
            return true;

        return false;
    }

    // Checks if the player is within the enemies line of sight
    private bool InLineOfSight()
    {
        if (Physics.Raycast(transform.position + transform.up, (Player.transform.position - transform.position), out RaycastHit hitInfo, shotRange))
        {
            if (hitInfo.transform.gameObject == Player)
            {
                return true;
            }
        }

        return false;
    }

    // If the shot cooldown has passed, and the player is within shooting range, and line of sight then shoot
    private void CheckShoot()
    {
        if (Time.time - _lastShotTime > shotCooldown && IsInShotRange() && InLineOfSight())
        {
            _lastShotTime = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        Transform myTransform = shootTransformation;
        GameObject projectile = Instantiate(projectilePrefab, myTransform.position + myTransform.forward + myTransform.up, myTransform.rotation);
        Vector3 direction = (Player.transform.position - transform.position).normalized; // Gets direction of player
        projectile.GetComponent<ShootingProjectile>().ProjectileInitialize(_projectileStats, direction);
    }
}
