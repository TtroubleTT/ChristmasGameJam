using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
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
        { AnimationType.RunLeft, "RunStrafeRLeft"},
        { AnimationType.Crouch, "StealthIdle"}
    };

    public void PlayPlayerAnimation(AnimationType type)
    {
        animator.Play(_animations[type]);
    }
}
