using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public PlayerData playerData;

    public ObservableInt distance;
    public int scoreIncreasedPerSecond = 10;
    public PlayerShip playerShip;

    private void Awake()
    {
        SaveManager.Instance.Load();
    }

    private void Update()
    {
        distance.Value += Mathf.CeilToInt(scoreIncreasedPerSecond * Time.deltaTime);
    }

    public void Reset()
    {
        distance.Value = 0;

        GameObject shipGO = Instantiate(playerData.currentShip.prefab, transform);
        playerShip = shipGO.GetComponent<PlayerShip>();
    }

}