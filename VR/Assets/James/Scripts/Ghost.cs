using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private void Update()
    {
        if (DifficultManager.instance.onNight)
        {
            Destroy(gameObject);
        }
    }
}
