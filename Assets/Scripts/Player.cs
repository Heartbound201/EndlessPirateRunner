using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Player : MonoBehaviour
{
    public UnityAction OnFatalHit;
    
    public ObservableInt Gold;
    public ObservableInt Distance;
    public int Lives = 1;

    public int scoreIncreasedPerSecond = 10;
    public ShipPrototype shipPrototype;
    private GameObject shipGameObject;

    private void Update()
    {
        Distance.Value += Mathf.CeilToInt(scoreIncreasedPerSecond * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>())
        {
            GetHit();
        }
        else if (other.gameObject.GetComponent<Enemy>())
        {
            GetHit();
        }
        else if(other.gameObject.GetComponent<Collectable>())
        {
            Collectable collectable = other.gameObject.GetComponent<Collectable>();
            Gold.Value += collectable.Worth;
            Destroy(other.gameObject);
            Debug.LogFormat("Collected {0} gold. {1} total", collectable.Worth, Gold);
        }
    }

    public void GetHit()
    {
        Lives--;
        if (Lives <= 0)
        {
            // TODO sunk animation
            OnFatalHit?.Invoke();
            Destroy(shipGameObject);
        }
    }

    public void Reset()
    {
        Gold.Value = 0;
        Distance.Value = 0;
        Lives = 1;

        shipGameObject = Instantiate(shipPrototype.prefab, transform);
    }
}