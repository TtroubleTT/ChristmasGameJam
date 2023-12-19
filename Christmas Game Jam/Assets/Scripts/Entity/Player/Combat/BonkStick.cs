using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkStick : MonoBehaviour, ICombat
{
    public float Damage { get; set; } = 100f;

    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Transform bonkUpRotatePoint;
    [SerializeField] private Transform bonkDownRotatePoint;
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;
    [SerializeField] private Animator bonkAnimator;

    private void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            Debug.Log("attack");
            bonkAnimator.Play("BonkStickSwing");
            Attack();
        }
    }

    public void Attack()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, bonkDownRotatePoint.rotation, rotateSpeed * Time.deltaTime);
    }
}
