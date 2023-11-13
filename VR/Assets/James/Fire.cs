using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireFX;
    [SerializeField] private Light fireLight;
    [SerializeField] private float fireTime;

    private void Update()
    {
        FireLife();
        
    }

    private void FireLife()
    {
        if (fireTime > 0)
        {
            fireTime -= Time.fixedDeltaTime;
        }
        else
        {
            fireTime = 0f;
        }

        if (fireTime < 30f)
        {
            fireLight.range = fireTime;
            fireFX.startSize = fireTime / 100;
        }
        else
        {
            fireLight.range = 30f;
            fireFX.startSize = 0.3f;
        }
    }
}
