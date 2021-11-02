using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IDamageable
{
    
    public int lives = 1;
    
    public virtual void GetHit()
    {
        lives--;
        if (lives <= 0)
        {
            // TODO sunk animation
            Destroy(gameObject);
        }
    }
}

public interface IDamageable
{
    public void GetHit();
}
