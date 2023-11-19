using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private MeshDestroy _meshDestroy;

    private void OnEnable()
    {
        _meshDestroy = GetComponent<MeshDestroy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _meshDestroy.DestroyMesh();
            _meshDestroy.enabled = false;
        }
    }

    
}