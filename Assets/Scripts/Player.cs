using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;

    public ObservableInt distance;
    public int scoreIncreasedPerSecond = 10;
    public PlayerShip playerShip;

    private bool _isIncrementingDistance;
    private void Start()
    {
        SaveManager.Instance.Load();
        _isIncrementingDistance = false;
    }

    private void Update()
    {
        if(_isIncrementingDistance) return;
        StartCoroutine(IncrementDistance());
    }

    private IEnumerator IncrementDistance()
    {
        _isIncrementingDistance = true;
        distance.Value += scoreIncreasedPerSecond;
        yield return new WaitForSeconds(1);
        _isIncrementingDistance = false;
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