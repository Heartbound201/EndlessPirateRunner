using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Obstacle, IDamageable
{
    public abstract void GetHit(int damage);
}