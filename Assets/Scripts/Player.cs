using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;

    public ObservableInt distance;
    public int scoreIncreasedPerSecond = 10;
    public PlayerShip playerShip;

    private void Start()
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
        if (playerShip != null)
        {
            Destroy(playerShip.gameObject);
        }
        GameObject shipGO = Instantiate(playerData.currentShip.prefab, transform);
        playerShip = shipGO.GetComponent<PlayerShip>();
        playerShip.lives.Value = playerData.currentShip.lives;
    }

}