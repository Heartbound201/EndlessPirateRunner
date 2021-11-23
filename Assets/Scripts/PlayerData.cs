using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public ObservableInt gold;
    public ObservableInt score;
    public ObservableInt lives;
    public int highscore;
    public PlayerShipPrototype currentShip;
    public PlayerShipPrototype startingShip;
    public HashSet<PlayerShipPrototype> unlockedShips = new HashSet<PlayerShipPrototype>();

}
