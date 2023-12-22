using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : EnemyBase
{
    protected override float MaxHealth { get; set; }
    
    protected override float CurrentHealth { get; set; }

    [Header("Enemy Stats")]
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float currentHealth = 50f;
    [SerializeField] private float shotRange = 30f;
    [SerializeField] private float shotCooldown = 3f;
    private float _lastShotTime;

    [Header("Projectile Stats")] 
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float range = 70f;
    
    [Header("References")] 
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootTransformation;
    [SerializeField] private CharacterController controller;
    private GameObject _player;
    private Transform _playerTransform;

    // Projectile Stats
    public enum Stats
    {
        Damage = 0,
        Speed = 1,
        Range = 2,
    }
    
    private readonly Dictionary<Stats, float> _projectileStats = new();

    protected override void InitializeAbstractedStats()
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }

    private void InitializeStats()
    {
        _projectileStats.Add(Stats.Damage, damage);
        _projectileStats.Add(Stats.Speed, speed);
        _projectileStats.Add(Stats.Range, range);
    }

    private void Start()
    {
        InitializeStats();
        InitializeAbstractedStats();
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _player.transform;
    }

    private void Update()
    {
        LookAtPlayer();
        MoveTowardsPlayer();
        CheckShoot();
    }
    
    // Enemy's look towards players
    private void LookAtPlayer()
    {
        Vector3 playerPos = _playerTransform.position;
        Vector3 lookPoint = new Vector3(playerPos.x, transform.position.y, playerPos.z);
        transform.LookAt(lookPoint);
    }

    private void MoveTowardsPlayer()
    {
        Vector3 pos = transform.position;
        Vector3 direction = (_player.transform.position - pos).normalized; // Gets direction of player
        direction = new Vector3(direction.x, 0, direction.z);
        controller.Move(direction * (15 * Time.deltaTime)); // Moving in the direction of move at the speed
    }

    // Checks if the distance between player and enemy is within the range they are allowed to fire
    private bool IsInRange()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance <= shotRange)
            return true;

        return false;
    }

    // Checks if the player is within the enemies line of sight
    private bool InLineOfSight()
    {
        if (Physics.Raycast(transform.position + transform.up, (_player.transform.position - transform.position), out RaycastHit hitInfo, shotRange))
        {
            if (hitInfo.transform.gameObject == _player)
            {
                return true;
            }
        }

        return false;
    }

    // If the shot cooldown has passed, and the player is within shooting range, and line of sight then shoot
    private void CheckShoot()
    {
        if (Time.time - _lastShotTime > shotCooldown && IsInRange() && InLineOfSight())
        {
            _lastShotTime = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        Transform myTransform = shootTransformation;
        GameObject projectile = Instantiate(projectilePrefab, myTransform.position + (myTransform.forward * 2) + myTransform.up, myTransform.rotation);
        Vector3 direction = (_player.transform.position - transform.position).normalized; // Gets direction of player
        projectile.GetComponent<ShootingProjectile>().ProjectileInitialize(_projectileStats, direction);
    }
}
