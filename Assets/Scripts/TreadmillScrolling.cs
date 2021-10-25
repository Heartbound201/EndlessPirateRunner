using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillScrolling : MonoBehaviour
{
    private float _scrollingSpeed;
    public float steeringSpeed = 0.5f;
    public float startingScrollingSpeed = 50f;
    public float scrollingAcceleration = 2f;
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
        
        // _scrollingSpeed += Time.deltaTime * scrollingAcceleration;
        CheckInput();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Translate(Vector3.back * _scrollingSpeed * Time.deltaTime);
        }
    }
    
    private void CheckInput()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                Move(Vector3.right);
            }
            else if (touch.position.x > Screen.width / 2)
            {
                Move(Vector3.left);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector3.right);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Move(Vector3.left);
        }
    }

    private void Move(Vector3 dir)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Translate(dir * steeringSpeed);
        }
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
