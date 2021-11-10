using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

class DestroyableObstacle : Obstacle, IDamageable
{
    
    public int lives;
    public DropSystem dropSystem;
    public void GetHit(int damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            // TODO sunk animation
            dropSystem.DropReward();
            Destroy(gameObject);
            
        }
    }
}