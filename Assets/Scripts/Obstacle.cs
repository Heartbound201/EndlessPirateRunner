using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Obstacle : Entity
{
    public int collisionDamage;
    
    public virtual void Collide(IDamageable damageable)
    {
        Debug.Log("Colliding with " + name);
        damageable.GetHit(collisionDamage);
    }
}