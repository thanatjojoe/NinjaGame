using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int objectHp;
    private MeshDestroy _meshDestroy;

    private void OnEnable()
    {
        _meshDestroy = GetComponent<MeshDestroy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HitObject"))
        {
            if (objectHp > 0)
            {
                objectHp -= 1;
            }
            else
            {
                _meshDestroy.DestroyMesh();
                _meshDestroy.enabled = false;
            }
        }
    }

    
}