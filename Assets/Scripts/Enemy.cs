using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    // TODO change to singleton
    public Transform plane;
    public int Lives = 1;
    public CollectablePrototype reward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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
            o.transform.SetParent(plane);
        }
    }
}