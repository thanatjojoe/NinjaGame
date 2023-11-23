using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultManager : MonoBehaviour
{
    public static DifficultManager instance;
    
    public int difficultLevel = 1;

    public bool onNight;
    
    public float spawnDelay;
    private float spawnDelayClone;

    public List<Transform> dayEnemy;
    public List<Transform> nightEnemy;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Update()
    {
        if (onNight)
        {
            spawnDelay = spawnDelayClone / 2;
        }
    }

    public void ChangeDifficult()
    {
        switch (difficultLevel)
        {
            case 1:
                spawnDelay = 20;
                spawnDelayClone = spawnDelay;
                break;
            case 2:
                spawnDelay = 18;
                spawnDelayClone = spawnDelay;
                break;
            case 3:
                spawnDelay = 16;
                spawnDelayClone = spawnDelay;
                break;
            case 4:
                spawnDelay = 14;
                spawnDelayClone = spawnDelay;
                break;
            case 5:
                spawnDelay = 12;
                spawnDelayClone = spawnDelay;
                break;
            case 6:
                spawnDelay = 10;
                spawnDelayClone = spawnDelay;
                break;
            case 7:
                spawnDelay = 8;
                spawnDelayClone = spawnDelay;
                break;
            default:
                spawnDelay = 6;
                spawnDelayClone = spawnDelay;
                break;
        }
    }
}

