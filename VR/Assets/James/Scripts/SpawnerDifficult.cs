using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnerDifficult : MonoBehaviour
{
    private DifficultManager _difficultManager;
    private bool onSpawn;
    private int beforeRandNumber = 1;
    

    private void Awake()
    {
        _difficultManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DifficultManager>();
        
    }

    private void Update()
    {
        SpawnerTimer();
    }

    private void SpawnerTimer()
    {
        if (!onSpawn)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        onSpawn = true;
        yield return new WaitForSeconds(Random.Range(1, 5));
        if (!_difficultManager.onNight)
        {
            Instantiate(_difficultManager.dayEnemy[RandomDayEnemy()],transform.position,Quaternion.identity);
        }
        else
        {
            Instantiate(_difficultManager.nightEnemy[RandomNightEnemy()],transform.position,Quaternion.identity);
        }
        yield return new WaitForSeconds(_difficultManager.spawnDelay);
        onSpawn = false;
    }

    private int RandomDayEnemy()
    {
        var rand = Random.Range(0,1);
        if (rand == beforeRandNumber)
        {
            switch (beforeRandNumber)
            {
                case 0:
                    rand = 1;
                    beforeRandNumber = 1;
                    break;
                case 1:
                    rand = 0;
                    beforeRandNumber = 0;
                    break;
            }
        }
        else
        {
            beforeRandNumber = rand;
        }
        return rand;
    }
    private int RandomNightEnemy()
    {
        var rand = Random.Range(0,2);
        if (rand == beforeRandNumber)
        {
            switch (beforeRandNumber)
            {
                case 0:
                    rand += 1;
                    beforeRandNumber = 1;
                    break;
                case 1:
                    rand = Random.Range(0, 1);
                    if (rand == 1)
                    {
                        rand = 2;
                        beforeRandNumber = 2;
                    }
                    else
                    {
                        rand = 0;
                        beforeRandNumber = 0;
                    }
                    break;
                case 2:
                    rand -= 1;
                    beforeRandNumber = 1;
                    break;
            }
        }
        else
        {
            beforeRandNumber = rand;
        }
        return rand;
    }

    
}
