using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float numberToSpawn;
    [SerializeField] private float timeBetweenSpawns;

    private void Start()
    {
        StartCoroutine(StartEnemySpawning());
    }

    IEnumerator StartEnemySpawning()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        Transform myTrans = transform;
        Instantiate(enemyPrefab, myTrans.position + myTrans.up, myTrans.rotation);
    }
}
