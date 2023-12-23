using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    [SerializeField] private KeyCode switchKey = KeyCode.Alpha1;
    [SerializeField] private GameObject bonkStick;
    [SerializeField] private GameObject laserGun;

    private void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            SwitchPlayerWeapon();
        }
    }

    private void SwitchPlayerWeapon()
    {
        if (bonkStick.activeSelf)
        {
            bonkStick.SetActive(false);
            laserGun.SetActive(true);
        }
        else
        {
            bonkStick.SetActive(true);
            laserGun.SetActive(false);
        }
    }
}
