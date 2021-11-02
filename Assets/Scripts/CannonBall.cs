using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.GetHit();
        }
        // TODO vfx sfx
        Destroy(gameObject);
    }
}
