using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

class DestroyableObstacle : Obstacle, IDamageable
{
    
    public int lives;
    [FormerlySerializedAs("rewards")] public List<Drop> drops = new List<Drop>();
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
        if (drops.Count > 0)
        {
            // TODO get weighted random reward
            Drop drop = drops[Random.Range(0, drops.Count)];
            GameObject o = Instantiate(drop.item.prefab, transform.position, Quaternion.identity);
            o.transform.SetParent(ScrollingPlane.Instance.transform);
        }
    }
}