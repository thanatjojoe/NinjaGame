using System.Collections;
using UnityEngine;
using System;

public class Enemy1 : MonoBehaviour
{
    public float speed = 5f; 
    public float jumpForce = 50f;
    public Rigidbody rigidbody;
    public Transform player; 
    private int enemyDmg = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //เดินหา player
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;

            // หันทิศทาง
            transform.forward = directionToPlayer.normalized; 
            
            // เคลื่อนที่ไปข้างหน้า
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
    void Bounce()
    {
        // กระเด็น
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        rigidbody.AddForce(Vector3.forward * jumpForce, ForceMode.Impulse);
    }
}