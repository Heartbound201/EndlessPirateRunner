using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public int Lives = 1;
    public CollectablePrototype reward;
    
    public void GetHit()
    {
        Lives--;
        if (Lives <= 0)
        {
            // TODO sunk animation
            DropReward();
            Destroy(gameObject);
            
        }
    }

    private void DropReward()
    {
        if (reward != null)
        {
            GameObject o = Instantiate(reward.prefab, transform.position, Quaternion.identity);
            o.transform.SetParent(ScrollingPlane.Instance.transform);
        }
    }
}