using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBall : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 10 * Time.deltaTime);
    }
    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.IncreaseSoul(100);
            Debug.Log("Add Soul");
            Destroy(gameObject);
        }
    }
}
