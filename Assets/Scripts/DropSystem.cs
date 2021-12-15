using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropSystem : MonoBehaviour
{
    public List<Drop> drops = new List<Drop>();

    private List<int> _prob = new List<int>();

    private Transform _env;

    private void Awake()
    {
        foreach (var drop in drops)
        {
            if (GameObjectPoolController.AddEntry(drop.item.poolKey, drop.item.prefab, 5, 10))
                Debug.Log("Pre-populating pool. key:" + drop.item.poolKey);
            else
                Debug.Log(drop.item.poolKey + "Pool already configured");
        }
    }

    private void Start()
    {
        _env = GameObject.FindGameObjectWithTag("Environment").transform;

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
            
            Poolable obj = GameObjectPoolController.Dequeue(drop.item.poolKey);
            var spawnPos = transform.position;
            obj.transform.position = new Vector3(spawnPos.x, 0, spawnPos.z);
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(_env);
        }
    }

    private Drop PickDrop()
    {
        return drops[_prob[Random.Range(0, _prob.Count)]];
    }
}