using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnemy : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
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
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
