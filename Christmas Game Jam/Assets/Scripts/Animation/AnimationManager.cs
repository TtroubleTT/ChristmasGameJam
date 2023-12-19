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
        { AnimationType.RunRight, "RunStrafeRight"},
        { AnimationType.RunLeft, "RunStrafeLeft"},
        { AnimationType.Crouch, "StealthIdle"}
    };

    private Dictionary<PlayerMovement.MovementState, AnimationManager.AnimationType> _movements = new()
    {
        { PlayerMovement.MovementState.Idle, AnimationType.Idle},
        { PlayerMovement.MovementState.Walking, AnimationType.WalkForward},
        { PlayerMovement.MovementState.WalkingBackwards, AnimationType.WalkBackward},
        { PlayerMovement.MovementState.WalkingRight, AnimationType.WalkRight},
        { PlayerMovement.MovementState.WalkingLeft, AnimationType.WalkLeft},
        { PlayerMovement.MovementState.Sprinting, AnimationType.RunForward},
        { PlayerMovement.MovementState.Crouching, AnimationType.Crouch},
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
