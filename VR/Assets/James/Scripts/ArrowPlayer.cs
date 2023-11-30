using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Body.Input;
using UnityEngine;

public class ArrowPlayer : MonoBehaviour
{
    public float speed = 10f;

    public Transform tip;

    private Rigidbody _rigidbody;

    private bool inAir = false;

    private Vector3 _lastPosition = Vector3.zero;


    private ParticleSystem _particleSystem;
    private TrailRenderer _trailRenderer;

    // Start is called before the first frame update
    private void Awake()
    {
        
        _rigidbody = GetComponent<Rigidbody>();

        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        
        Pull.PullActionReleased += Release;

        Stop();
    }

    private void OnDestroy()
    {
        Pull.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        Pull.PullActionReleased -= Release;
        gameObject.transform.parent = null;
        inAir = true;
        SetPhysics(true);

        Vector3 force = transform.forward * value * speed;
        _rigidbody.AddForce(force, ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity());
        
        _particleSystem.Play();
        _trailRenderer.emitting = true;

    }

    private void Stop()
    {
        inAir = false;
        SetPhysics(false);
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidbody.useGravity = usePhysics;
        _rigidbody.isKinematic = !usePhysics;
    }

    private void CheckCollision()
    {
        if (Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.TryGetComponent(out  Rigidbody body))
            {
                _rigidbody.interpolation = RigidbodyInterpolation.None;
                transform.parent = hitInfo.transform;
                body.AddForce(_rigidbody.velocity, ForceMode.Impulse);
            }
            //Stop();
            
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            SoundManager.instance.Play(SoundManager.SoundName.enemyHurt);
            
        }
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidbody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    void FixedUpdate()
    {
        if (inAir)
        {
            CheckCollision();
            _lastPosition = tip.position;
        }
    }
}
