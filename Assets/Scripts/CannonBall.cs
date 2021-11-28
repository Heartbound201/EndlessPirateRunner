using System;
using System.Collections;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int damage = 1;
    public GameObject explosion;
    // public AudioClipSO explosionSfx;
    public GameObject waterSplash;
    // public AudioClipSO waterSplashSfx;
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

    private void Update()
    {
        if (transform.position.y <= 0)
        {
            Instantiate(waterSplash, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
