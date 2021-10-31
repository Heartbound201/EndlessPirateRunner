using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    
    public ObservableInt Gold;
    public ObservableInt Distance;

    public int scoreIncreasedPerSecond = 10;
    public ShipPrototype shipPrototype;
    public PlayerShip playerShip;


    private void Update()
    {
        Distance.Value += Mathf.CeilToInt(scoreIncreasedPerSecond * Time.deltaTime);
    }

    public void Reset()
    {
        Gold.Value = 0;
        Distance.Value = 0;

        GameObject shipGO = Instantiate(shipPrototype.prefab, transform);
        playerShip = shipGO.GetComponent<PlayerShip>();
    }
}