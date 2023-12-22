using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour, ICombat
{
    public float Damage { get; set; }

    [Header("References")] 
    [SerializeField] private Transform camTrans;
    [SerializeField] private GameObject projectilePrefab;
    
    [Header("Input")] 
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;

    [Header("Stats")]
    [SerializeField] private float attackCooldown = .5f;
    private float _lastAttack;
    
    [Header("Projectile Stats")] 
    [SerializeField] private float damage = 40f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float range = 120f;
    
    private readonly Dictionary<ShootingEnemy.Stats, float> _projectileStats = new();

    private void InitializeAbstractedStats()
    {
        Damage = damage;
    }
    
    private void InitializeStats()
    {
        _projectileStats.Add(ShootingEnemy.Stats.Damage, Damage);
        _projectileStats.Add(ShootingEnemy.Stats.Speed, speed);
        _projectileStats.Add(ShootingEnemy.Stats.Range, range);
    }

    private void Start()
    {
        InitializeAbstractedStats();
        InitializeStats();
    }

    private void Update()
    {
        CheckAttack();
    }

    private void CheckAttack()
    {
        // if you are pressing attack key and its passed the cooldown
        if (Input.GetKey(attackKey) && Time.time - _lastAttack > attackCooldown)
        {
            Attack();
        }
    }

    public void Attack()
    {
        _lastAttack = Time.time;
        GameObject projectile = Instantiate(projectilePrefab, camTrans.position + (camTrans.forward * 2), camTrans.rotation);
        Vector3 direction = camTrans.forward.normalized; // Gets direction player is looking
        projectile.GetComponent<ShootingProjectile>().ProjectileInitialize(_projectileStats, direction);
    }
}
