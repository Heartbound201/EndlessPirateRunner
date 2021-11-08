using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Collectable : Entity
{
    public virtual void Collect()
    {
        Debug.Log("Collecting " + name);
    }
    
}