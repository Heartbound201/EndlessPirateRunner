using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPlane : MonoBehaviour
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
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                return Vector3.right * steeringSpeed;
            }
            else if (touch.position.x > Screen.width / 2)
            {
                return Vector3.left * steeringSpeed;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            return Vector3.right * steeringSpeed;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            return Vector3.left * steeringSpeed;
        }
        return Vector3.zero;
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
    
}
