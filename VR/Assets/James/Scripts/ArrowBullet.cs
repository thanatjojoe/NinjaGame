using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBullet : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 10f; // Adjust the speed of the bullet

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.up * arrowSpeed * Time.deltaTime);

        // Destroy the bullet after a certain time or distance
        Destroy(gameObject, 5f);
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
