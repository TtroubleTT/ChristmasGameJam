using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isPlayer;
    private PlayerMovement _playerMovement;
    
    public enum PlayerAnimationType
    {
        Idle,
        WalkForward,
        WalkBackward,
        WalkRight,
        WalkLeft,
        RunForward,
        RunBackward,
        RunRight,
        RunLeft,
        Crouch,
        CrouchWalkForward,
        CrouchWalkBackward,
        Jump,
    }
    
    public enum SantaAnimationType
    {
        Idle1,
        Idle2,
        DementedWave,
        Walk,
        Slap,
        Beam,
        BeamLoop,
        FallOver,
        Morphing,
        MonsterRun,
        Ball,
        BallRoll,
    }

    private Dictionary<AnimationManager.PlayerAnimationType, string> _playerAnimations = new()
    {
        { PlayerAnimationType.Idle , "Idle1" },
        { PlayerAnimationType.WalkForward, "Walk"},
        { PlayerAnimationType.WalkBackward, "WalkBack"},
        { PlayerAnimationType.WalkRight, "WalkStrafeRight"},
        { PlayerAnimationType.WalkLeft, "WalkStrafeLeft"},
        { PlayerAnimationType.RunForward, "Run"},
        { PlayerAnimationType.RunBackward, "RunBack"},
        { PlayerAnimationType.RunRight, "RunStrafeRightFront"},
        { PlayerAnimationType.RunLeft, "RunStrafeLeftFront"},
        { PlayerAnimationType.Crouch, "StealthIdle"},
        { PlayerAnimationType.CrouchWalkForward, "StealthWalk"},
        { PlayerAnimationType.CrouchWalkBackward, "StealthWalkBack"},
        { PlayerAnimationType.Jump, "Freefall"}
    };

    private Dictionary<PlayerMovement.MovementState, AnimationManager.PlayerAnimationType> _playerMovements = new()
    {
        { PlayerMovement.MovementState.Idle, PlayerAnimationType.Idle},
        { PlayerMovement.MovementState.Walking, PlayerAnimationType.WalkForward},
        { PlayerMovement.MovementState.WalkingBackwards, PlayerAnimationType.WalkBackward},
        { PlayerMovement.MovementState.WalkingRight, PlayerAnimationType.WalkRight},
        { PlayerMovement.MovementState.WalkingLeft, PlayerAnimationType.WalkLeft},
        { PlayerMovement.MovementState.Sprinting, PlayerAnimationType.RunForward},
        { PlayerMovement.MovementState.SprintingBackwards, PlayerAnimationType.RunBackward},
        { PlayerMovement.MovementState.SprintingRight, PlayerAnimationType.RunRight},
        { PlayerMovement.MovementState.SprintingLeft, PlayerAnimationType.RunLeft},
        { PlayerMovement.MovementState.Crouching, PlayerAnimationType.Crouch},
        { PlayerMovement.MovementState.CrouchWalkForward, PlayerAnimationType.CrouchWalkForward},
        { PlayerMovement.MovementState.CrouchWalkBackWard, PlayerAnimationType.CrouchWalkBackward},
        { PlayerMovement.MovementState.Air, PlayerAnimationType.Jump},
    };
    
    private Dictionary<AnimationManager.SantaAnimationType, string> _santaAnimations = new()
    {
        { SantaAnimationType.Idle1, "Bip001|Santa Idle 1" },
        { SantaAnimationType.Idle2, "Bip001|Santa Idle 2" },
        { SantaAnimationType.DementedWave, "Bip001|Demented Wave" },
        { SantaAnimationType.Walk, "Bip001|Santa Walk" },
        { SantaAnimationType.Slap, "Bip001|SLAP" },
        { SantaAnimationType.Beam, "Bip001|Santa Beam" },
        { SantaAnimationType.BeamLoop, "Bip001|Santa Beam Loop" },
        { SantaAnimationType.FallOver, "Bip001|Fall over" },
        { SantaAnimationType.Morphing, "Bip001|Morphing" },
        { SantaAnimationType.MonsterRun, "Bip001|Monster Run" },
        { SantaAnimationType.Ball, "Bip001|Santa Ball" },
        { SantaAnimationType.BallRoll, "Bip001|Santa Ball Rolling" },
    };

    private void Start()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!isPlayer)
            return;
        
        if (!_playerMovements.ContainsKey(_playerMovement.movementState))
            return;
        
        AnimationStateManager(_playerMovement.movementState);
    }

    private void AnimationStateManager(PlayerMovement.MovementState state)
    {
        PlayPlayerAnimation(_playerMovements[state]);
    }

    public void PlayPlayerAnimation(PlayerAnimationType type)
    {
        animator.Play(_playerAnimations[type]);
    }
    
    public void PlaySantaAnimation(SantaAnimationType type)
    {
        animator.Play(_santaAnimations[type]);
    }
}
