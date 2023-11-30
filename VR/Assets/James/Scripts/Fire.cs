using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireFX;
    [SerializeField] private Light fireLight;
    public float fireTime;
    public bool isShop;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stick") && !isShop)
        {
            fireTime += 45f;
            Destroy(other.gameObject);
        }
    }
}
