using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    public List<Drop> drops = new List<Drop>();

    private List<int> _prob = new List<int>();

    private void Start()
    {
        for (int i = 0; i < drops.Count; i++)
        {
            for (int j = 0; j < drops[i].weight; j++)
            {
                _prob.Add(i);
            }
        }
        
    }

    public void DropReward()
    {
        if (drops.Count > 0)
        {
            Drop drop = PickDrop();
            GameObject o = Instantiate(drop.item.prefab, transform.position, Quaternion.identity);
            ScrollingPlane.Instance.Spawn(o);
            // o.transform.SetParent(ScrollingPlane.Instance.transform);
        }
    }

    private Drop PickDrop()
    {
        return drops[_prob[Random.Range(0, _prob.Count)]];
    }
}