using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHP : MonoBehaviour
{
    private float enemyHp = 5;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private NavMeshAgent _agent;
    public bool isDead;
    private bool onStun;
    public Transform soulPoint;
    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponPlayer"))
        {
            Dead();
        }
    }*/

    public void Dead()
    {
        _rigidbody.isKinematic = false;
        _animator.enabled = false;
        _agent.enabled = false;
        isDead = true;
        Instantiate(soulPoint, transform.position, Quaternion.identity);
        StartCoroutine(DestoryTime());
    }

    public void Stun()
    {
        if (!onStun)
        {
            StartCoroutine(EnemyStun());
        }
    }

    IEnumerator EnemyStun()
    {
        onStun = true;
        Debug.Log("Enemy Stun");
        yield return new WaitForSeconds(1.5f); //เวลาที่จะติดสตั้น
        onStun = false;
    }

    IEnumerator DestoryTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
