using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : MonoBehaviour
{
    [Header("Detect")] 
    [SerializeField] private SphereCollider detectCollider;
    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float lookAtSpeed = 10f;

    [Header("Combat")]
    [SerializeField] private float attackDelay = 3f;

    private bool onAttack;
    private bool onPlayer;
    
    private Rigidbody _rigidbody;
    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform playerTransform;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        detectCollider = GetComponent<SphereCollider>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {

        if (!GameManager.instance.IsGameOver)
        {
            AttackMode();
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
            if (!onAttack)
            {
                _animator.SetBool("Run",true);
                _agent.SetDestination(playerTransform.position);
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
        _agent.enabled = false;
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
        _animator.SetTrigger($"Attack");
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
