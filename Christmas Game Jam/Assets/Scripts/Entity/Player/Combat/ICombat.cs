using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{
    public float Damage { get; set; }

    public void Attack();
}
