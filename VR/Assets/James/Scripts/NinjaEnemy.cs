using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NinjaEnemy : MonoBehaviour
{
    [SerializeField] private Transform detectTrans;
    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask playerLayer;

    private bool onAttack;
    
    private Rigidbody _rigidbody;
    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform playerTransform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        _agent.SetDestination(playerTransform.position);
        AttackMode();
    }

    private void AttackMode()
    {
        Collider[] playerDetect = Physics.OverlapSphere(detectTrans.position, detectRadius, playerLayer);
        foreach (Collider player in playerDetect)
        {
            if (player.gameObject.CompareTag("Player"))
            {
                if (!onAttack)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("HitObject"))
        {
            Dead();
        }
    }

    private void Dead()
    {
        _rigidbody.isKinematic = false;
        _animator.enabled = false;
        StartCoroutine(DestoryTime());
    }

    IEnumerator DestoryTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    IEnumerator Attack()
    {
        onAttack = true;
        Debug.Log("Attack!!!");
        yield return new WaitForSeconds(5f);
        onAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectTrans.position,detectRadius);
    }
}
