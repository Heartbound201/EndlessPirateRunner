using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "Prototype/Player Ship")]
public class PlayerShipPrototype : EntityPrototype
{
    public int cost;
    public Sprite sprite;
    public bool hasCannons;
    public int lives;
}
