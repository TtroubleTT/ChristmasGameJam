using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public enum AnimationType
    {
        WalkForward,
        WalkBackward,
        WalkRight,
        WalkLeft,
        Crouch,
    }

    private Dictionary<AnimationManager.AnimationType, string> _animations = new()
    {
        { AnimationType.WalkForward, "Walk"},
        { AnimationType.WalkBackward, "Walk"}
    };
}
