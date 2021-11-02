using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    
    public ObservableInt gold;
    public ObservableInt distance;

    public int HighScore
    {
        get
        {
            if (_saveState != null)
            {
                return _saveState.highscore;
            }

            return 0;
        }
    }

    public int scoreIncreasedPerSecond = 10;
    public ShipPrototype currentShip;
    public ShipPrototype startingShip;
    public List<ShipPrototype> availableShips = new List<ShipPrototype>();
    public PlayerShip playerShip;

    private SaveState _saveState = new SaveState();

    private void Awake()
    {
        Load();
    }

    private void Update()
    {
        distance.Value += Mathf.CeilToInt(scoreIncreasedPerSecond * Time.deltaTime);
    }

    public void Reset()
    {
        gold.Value = _saveState.gold;
        distance.Value = 0;

        GameObject shipGO = Instantiate(currentShip.prefab, transform);
        playerShip = shipGO.GetComponent<PlayerShip>();
    }

    public void Save()
    {
        _saveState.gold = gold.Value;
        if(distance.Value > _saveState.highscore)
            _saveState.highscore = distance.Value;
        _saveState.currentShip = currentShip;
        _saveState.unlockedShips = availableShips;
        
        FileManager.WriteToFile(_saveState);
    }

    public void Load()
    {
        _saveState = FileManager.ReadFromFile();

        gold.Value = _saveState.gold;
        currentShip = _saveState.currentShip;
        availableShips = _saveState.unlockedShips;
        if (currentShip == null)
        {
            currentShip = startingShip;
        }
        
    }
}