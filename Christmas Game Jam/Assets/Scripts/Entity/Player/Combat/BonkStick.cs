using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkStick : MonoBehaviour, ICombat
{
    public float Damage { get; set; }
    
    [Header("References")]
    [SerializeField] private Animator bonkAnimator;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform cam;
    
    [Header("Attack")] 
    [SerializeField] private float damage = 100f;
    [SerializeField] private float attackDistance = 6f;
    [SerializeField] private float attackWidth = 2.5f;
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;
    [SerializeField] private float cooldown = 1f;
    private float _lastAttack;

    private void Start()
    {
        Damage = damage;
    }

    private void Update()
    {
        if (Input.GetKeyDown(attackKey) && Time.time - _lastAttack >= cooldown)
        {
            _lastAttack = Time.time;
            bonkAnimator.Play("BonkStickSwing");
            Attack();
        }
    }

    public void Attack()
    {
        Vector3 camPos = cam.position;
        Vector3 camforward = cam.forward;
        Quaternion camRot = cam.rotation;

        bool hitEnemy = Physics.BoxCast(camPos + (-camforward * 2.5f), new Vector3(attackWidth, attackWidth, attackWidth), camforward, out RaycastHit hitInfo, camRot, attackDistance, enemyLayer);
        if (hitEnemy)
        {
            // If there is a better way to do this please tell me
            hitInfo.transform.gameObject.GetComponent<EnemyBase>().SubtractHealth(Damage);
        }
    }
}
