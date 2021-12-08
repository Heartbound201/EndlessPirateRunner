using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestructor : MonoBehaviour
{
    [SerializeField] private float m_TimeOut = 1.0f;
    [SerializeField] private bool m_DetachChildren = false;


    private void OnEnable()
    {
        Invoke("DestroyNow", m_TimeOut);
    }


    private void DestroyNow()
    {
        if (m_DetachChildren)
        {
            transform.DetachChildren();
        }

        Poolable poolable = gameObject.GetComponent<Poolable>();
        if (poolable)
        {
            GameObjectPoolController.Enqueue(poolable);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}