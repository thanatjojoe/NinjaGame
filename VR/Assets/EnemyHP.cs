using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHP : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    private NavMeshAgent _agent;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponPlayer"))
        {
            Dead();
        }
    }

    private void Dead()
    {
        _rigidbody.isKinematic = false;
        _animator.enabled = false;
        _agent.enabled = false;
        StartCoroutine(DestoryTime());
    }

    IEnumerator DestoryTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
