using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CannonBall : MonoBehaviour
{
    public int damage = 1;
    public float gravity;
    public GameObject explosion;
    // public AudioClipSO explosionSfx;
    public GameObject waterSplash;
    // public AudioClipSO waterSplashSfx;

    private Rigidbody _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.GetHit(damage);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(new Vector3(0, -1.0f, 0) * _rb.mass * gravity); 
    }

    private void Update()
    {
        if (transform.position.y <= 0)
        {
            Instantiate(waterSplash, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
