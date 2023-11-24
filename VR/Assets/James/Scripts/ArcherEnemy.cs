using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherEnemy : MonoBehaviour
{
   [Header("Detect")] 
    [SerializeField] private SphereCollider detectCollider;
    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float lookAtSpeed = 10f;

    [Header("Combat")]
    [SerializeField] private float attackDelay = 3f;
    [SerializeField] private GameObject arrowObject;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform arrowPrefab;

    private bool onAttack;
    private bool onPlayer;
    
    private Animator _animator;
    private NavMeshAgent _agent;
    private EnemyHP _enemyHp;
    private Transform playerTransform;
    
    private GameManager _gameManager;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        detectCollider = GetComponent<SphereCollider>();
        _enemyHp = GetComponent<EnemyHP>();
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _gameManager    = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    private void Update()
    {
        detectCollider.radius = detectRadius;
        _agent.stoppingDistance = detectRadius;
        if (!GameManager.instance.IsGameOver)
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
        
        if (onPlayer)
        { 
            _animator.SetBool("Run",false);
            _agent.isStopped = true;
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
                _agent.isStopped = false;
                _animator.SetBool("Run",true);
                _agent.SetDestination(playerTransform.position);
            }
            
        }
    }

    IEnumerator Attack()
    {
        onAttack = true;
        _animator.SetTrigger($"Hold");
        ArrowAppear();
        yield return new WaitForSeconds(attackDelay);
        _animator.SetTrigger("Release");
        ShootArrow();
        yield return new WaitForSeconds(attackDelay);
        onAttack = false;
    }

    public void ArrowAppear()
    {
        arrowObject.SetActive(true);
    }

    public void ShootArrow()
    {
        arrowObject.SetActive(false);
        Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
    }
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
