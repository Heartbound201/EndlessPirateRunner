using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WindyBehaviour : WeatherBehaviour
{
    public string warningMessage;
    public Text warningText;
    public int windStrengthMin;
    public int windStrengthMax;
    private ScrollingPlane _scrollingPlane;
    private Vector3 _windVelocity;

    private void Start()
    {
        _scrollingPlane = FindObjectOfType<ScrollingPlane>();
    }

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
        for (int i = 0; i < _scrollingPlane.transform.childCount; i++)
        {
            _scrollingPlane.transform.GetChild(i).Translate(_windVelocity * Time.deltaTime);
        }
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