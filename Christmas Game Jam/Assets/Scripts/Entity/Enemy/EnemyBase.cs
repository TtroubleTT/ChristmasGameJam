using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EnemyBase : EntityBase
{
    protected override void Die()
    {
        Destroy(gameObject);
    }
}
