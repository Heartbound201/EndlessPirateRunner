using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ScrollingPlane : MonoSingleton<ScrollingPlane>
{
    private float _scrollingSpeed;
    public float steeringSpeed = 0.5f;
    public float startingScrollingSpeed = 50f;
    public GameObject isle;
    public bool IsScrolling;

    private void Start()
    {
        IsScrolling = false;
        Reset();
    }

    public void Reset()
    {
        ResetSpawnedObjects();
        ResetScrollingSpeed();
        SpawnIsle();
    }

    void Update()
    {
        if(!IsScrolling) return;
        
        Vector3 lateralVector = Steer();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Translate(Vector3.back * _scrollingSpeed * Time.deltaTime + lateralVector * Time.deltaTime);
            // Transform child = transform.GetChild(i);
            // Rigidbody component = child.GetComponent<Rigidbody>();
            // component.velocity = Vector3.back * _scrollingSpeed + lateralVector;
        }
    }
    
    private Vector3 Steer()
    {
        var axis = CrossPlatformInputManager.GetAxis("Horizontal");
        return Vector3.left * axis * steeringSpeed;
    }

    private void ResetScrollingSpeed()
    {
        _scrollingSpeed = startingScrollingSpeed;
    }

    private void ResetSpawnedObjects()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void SpawnIsle()
    {
        Instantiate(isle, transform);
    }
    
    public void StartScrolling(){}
    public void StopScrolling(){}
    public void StartGenerating(){}
    public void StpoGenerating(){}
    
    
    
}
