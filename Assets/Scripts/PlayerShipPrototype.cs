using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "Prototype/Player Ship")]
public class PlayerShipPrototype : EntityPrototype
{
    public int cost;
    public Sprite sprite;
    public bool hasCannons;
    public float lateralSpeed;
    public float forwardSpeed;
    public int lives;
    public float graceTime;
    public int scoreIncreasedPerSecond;
    public float firingCooldown;
    public float targetIndicatorSpeed;
}
