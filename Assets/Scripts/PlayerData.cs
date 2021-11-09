using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public ObservableInt gold;
    public int highscore;
    public PlayerShipPrototype currentShip;
    public PlayerShipPrototype startingShip;
    public HashSet<PlayerShipPrototype> unlockedShips = new HashSet<PlayerShipPrototype>();

}
