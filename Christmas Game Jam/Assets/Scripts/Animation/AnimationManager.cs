using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;
    
    public enum AnimationType
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

    private Dictionary<AnimationManager.AnimationType, string> _animations = new()
    {
        { AnimationType.Idle , "Idle1" },
        { AnimationType.WalkForward, "Walk"},
        { AnimationType.WalkBackward, "WalkBack"},
        { AnimationType.WalkRight, "WalkStrafeRight"},
        { AnimationType.WalkLeft, "WalkStrafeLeft"},
        { AnimationType.RunForward, "Run"},
        { AnimationType.RunBackward, "RunBack"},
        { AnimationType.RunRight, "RunStrafeRightFront"},
        { AnimationType.RunLeft, "RunStrafeLeftFront"},
        { AnimationType.Crouch, "StealthIdle"},
        { AnimationType.CrouchWalkForward, "StealthWalk"},
        { AnimationType.CrouchWalkBackward, "StealthWalkBack"},
        { AnimationType.Jump, "Freefall"}
    };

    private Dictionary<PlayerMovement.MovementState, AnimationManager.AnimationType> _movements = new()
    {
        { PlayerMovement.MovementState.Idle, AnimationType.Idle},
        { PlayerMovement.MovementState.Walking, AnimationType.WalkForward},
        { PlayerMovement.MovementState.WalkingBackwards, AnimationType.WalkBackward},
        { PlayerMovement.MovementState.WalkingRight, AnimationType.WalkRight},
        { PlayerMovement.MovementState.WalkingLeft, AnimationType.WalkLeft},
        { PlayerMovement.MovementState.Sprinting, AnimationType.RunForward},
        { PlayerMovement.MovementState.SprintingBackwards, AnimationType.RunBackward},
        { PlayerMovement.MovementState.SprintingRight, AnimationType.RunRight},
        { PlayerMovement.MovementState.SprintingLeft, AnimationType.RunLeft},
        { PlayerMovement.MovementState.Crouching, AnimationType.Crouch},
        { PlayerMovement.MovementState.CrouchWalkForward, AnimationType.CrouchWalkForward},
        { PlayerMovement.MovementState.CrouchWalkBackWard, AnimationType.CrouchWalkBackward},
        { PlayerMovement.MovementState.Air, AnimationType.Jump},
    };

    private void Update()
    {
        if (!_movements.ContainsKey(playerMovement.movementState))
            return;
        
        AnimationStateManager(playerMovement.movementState);
    }

    private void AnimationStateManager(PlayerMovement.MovementState state)
    {
        PlayPlayerAnimation(_movements[state]);
    }

    public void PlayPlayerAnimation(AnimationType type)
    {
        animator.Play(_animations[type]);
    }
}
