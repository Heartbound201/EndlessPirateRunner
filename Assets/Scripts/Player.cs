using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;

    public PlayerShip playerShip;

    private void Start()
    {
        SaveManager.Instance.Load();
    }

    private void Update()
    {
        if (!playerShip)
        {
            return;
        }

        transform.position = playerShip.transform.position;
    }


    public void Reset()
    {
        transform.position = Vector3.zero;
        playerData.score.Value = 0;
        if (playerShip != null)
        {
            Destroy(playerShip.gameObject);
        }

        playerShip = BuildShip(playerData.currentShip);
    }

    private PlayerShip BuildShip(PlayerShipPrototype prototype)
    {
        GameObject shipGO = Instantiate(playerData.currentShip.prefab, transform.position, Quaternion.identity);
        
        PlayerShip playerShipComponent = shipGO.GetComponent<PlayerShip>();
        playerShipComponent.lateralSpeed = prototype.lateralSpeed;
        playerShipComponent.forwardSpeed = prototype.forwardSpeed;
        playerShipComponent.playerData = playerData;
        playerShipComponent.graceTime = prototype.graceTime;
        playerShipComponent.scoreIncreasedPerSecond = prototype.scoreIncreasedPerSecond;
        playerShipComponent.maxLives = prototype.lives;

        if(prototype.hasCannons)
        {
            playerShipComponent.cannonSystem.firingCooldown = prototype.firingCooldown;
        }
        
        playerData.lives.Value = playerShipComponent.maxLives;

        return playerShipComponent;
    }

}