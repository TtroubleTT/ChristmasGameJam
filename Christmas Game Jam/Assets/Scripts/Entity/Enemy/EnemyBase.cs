using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EnemyBase : EntityBase
{
    protected abstract float DistanceToKeepFromPlayer { get; set; }
    
    protected abstract float MovementSpeed { get; set; }
    
    protected abstract CharacterController CharacterController { get; set; }

    protected GameObject Player;
    private Transform _playerTransform;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = Player.transform;
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
    
    // Rotates enemy to look at the player
    protected virtual void LookAtPlayer()
    {
        Vector3 playerPos = _playerTransform.position;
        Vector3 lookPoint = new Vector3(playerPos.x, transform.position.y, playerPos.z);
        transform.LookAt(lookPoint);
    }
    
    // Moves enemy towards player at its speed
    protected virtual void MoveTowardsPlayer()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);

        if (distance <= DistanceToKeepFromPlayer)
            return;
        
        Vector3 pos = transform.position;
        Vector3 direction = (Player.transform.position - pos).normalized; // Gets direction of player
        direction = new Vector3(direction.x, 0, direction.z);
        CharacterController.Move(direction * (MovementSpeed * Time.deltaTime)); // Moving in the direction of move at the speed
    }
}
