using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class NinjaEnemy : MonoBehaviour
{
    [Header("Detect")] 
    [SerializeField] private SphereCollider detectCollider;
    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float lookAtSpeed = 10f;

    [Header("Combat")]
    [SerializeField] private float attackDelay = 3f;

    public bool onAttack;
    private bool onPlayer;
    
    
    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform playerTransform;
    private EnemyHP _enemyHp;
    
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _enemyHp = GetComponent<EnemyHP>();
        detectCollider = GetComponent<SphereCollider>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {

        if (!GameManager.instance.IsGameOver && !_enemyHp.isDead)
        {
            AttackMode();
        }
        else
        {
            _agent.enabled = false;
        }




    }

    private void AttackMode()
    {
        detectCollider.radius = detectRadius;
        
        if (onPlayer)
        { 
            _animator.SetBool("Run",false);
            
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, lookAtSpeed * Time.deltaTime);
           
            if (!onAttack)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            if (!onAttack && !GameManager.instance.IsGameOver)
            {
                _animator.SetBool("Run",true);
                _agent.SetDestination(playerTransform.position);
            }
            
        }
    }

    

    IEnumerator Attack()
    {
        onAttack = true;
        int rand = Random.Range(1, 3);
        _animator.SetTrigger($"Attack{rand}");
        //Debug.Log("Attack!!!");
        yield return new WaitForSeconds(attackDelay);
        onAttack = false;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectTrans.position,detectRadius);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayer = false;
        }
    }
}
