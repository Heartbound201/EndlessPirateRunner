using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveState
{
    public int gold;
    public int highscore;
    public ShipPrototype currentShip;
    public List<ShipPrototype> unlockedShips = new List<ShipPrototype>();

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void FromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}