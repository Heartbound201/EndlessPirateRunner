using UnityEngine;
using UnityEngine.UI;

public class WindyBehaviour : WeatherBehaviour
{
    public Player player;
    public string warningMessage;
    public Text warningText;
    public int windStrengthMin;
    public int windStrengthMax;
    private Vector3 _windVelocity;

    private void OnEnable()
    {
        int windStrength = Random.Range(-windStrengthMax, windStrengthMax);
        if (windStrength < 0)
        {
            windStrength = Mathf.Clamp(windStrength, -windStrengthMax, -windStrengthMin);
        }
        else
        {
            windStrength = Mathf.Clamp(windStrength, windStrengthMin, windStrengthMax);
        }

        _windVelocity = new Vector3(windStrength, 0, 0);
    }

    private void FixedUpdate()
    {
        player.playerShip.rigidbody.AddForce(_windVelocity);
    }

    public override void Show()
    {
        base.Show();
        warningText.text = warningMessage;
        warningText.gameObject.SetActive(true);
    }

    public override void Hide()
    {
        base.Hide();
        warningText.gameObject.SetActive(false);
    }
}