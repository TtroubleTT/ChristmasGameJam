using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string stateName;

    private void Start()
    {
        animator.Play(stateName);
    }
}
