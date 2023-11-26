using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoxSpanwer : MonoBehaviour
{
    [SerializeField] private Transform destroyBox;

    public Enemy enemy;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("DestroyBox").GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemy == null)
        {
            Instantiate(destroyBox, transform.position, Quaternion.identity);
            enemy = GameObject.FindGameObjectWithTag("DestroyBox").GetComponent<Enemy>();
        }
    }
}
