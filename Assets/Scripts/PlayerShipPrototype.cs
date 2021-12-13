using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "Prototype/Player Ship")]
public class PlayerShipPrototype : EntityPrototype
{
    public PlayerData playerData;
    
    [Header("Shop")]
    public int cost;
    public Sprite sprite;
    
    [Header("Ship")]
    public float lateralSpeedMin;
    public float lateralSpeedMax;
    public float lateralSpeed;
    public float lateralSpeedLinearChange;
    
    public float forwardSpeedMin;
    public float forwardSpeedMax;
    public float forwardSpeed;
    public float forwardSpeedLinearChange;

    public int lives;
    public float graceTime;
    public int scoreIncreasedPerSecond;
    
    [Header("Cannons")]
    public bool hasCannons;
    public float firingCooldown;
    public float targetIndicatorSpeed;
    public float targetIndicatorRadius;

    public override GameObject Build(GameObject obj)
    {
        PlayerShip playerShipComponent = obj.GetComponent<PlayerShip>();
        
        playerShipComponent.lateralSpeedMin = lateralSpeedMin;
        playerShipComponent.lateralSpeedMax = lateralSpeedMax;
        playerShipComponent.lateralSpeed = lateralSpeed;
        playerShipComponent.lateralSpeedLinearChange = lateralSpeedLinearChange;
        
        playerShipComponent.forwardSpeedMin = forwardSpeedMin;
        playerShipComponent.forwardSpeedMax = forwardSpeedMax;
        playerShipComponent.forwardSpeed = forwardSpeed;
        playerShipComponent.forwardSpeedLinearChange = forwardSpeedLinearChange;
        
        playerShipComponent.playerData = playerData;
        playerShipComponent.graceTime = graceTime;
        playerShipComponent.scoreIncreasedPerSecond = scoreIncreasedPerSecond;
        playerShipComponent.maxLives = lives;

        if(hasCannons)
        {
            playerShipComponent.cannonSystem.firingCooldown = firingCooldown;
            playerShipComponent.cannonSystem.targetIndicatorSpeed = targetIndicatorSpeed;
            playerShipComponent.cannonSystem.targetIndicatorRadius = targetIndicatorRadius;
        }

        return obj;
    }
}
