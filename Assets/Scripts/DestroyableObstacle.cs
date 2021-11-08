using System.Collections.Generic;
using UnityEngine;

class DestroyableObstacle : Obstacle, IDamageable
{
    
    public int lives;
    public List<Reward> rewards = new List<Reward>();
    public void GetHit(int damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            // TODO sunk animation
            DropReward();
            Destroy(gameObject);
            
        }
    }
    private void DropReward()
    {
        if (rewards.Count > 0)
        {
            // TODO get weighted random reward
            GameObject o = Instantiate(rewards[1].item.prefab, transform.position, Quaternion.identity);
            o.transform.SetParent(ScrollingPlane.Instance.transform);
        }
    }
}