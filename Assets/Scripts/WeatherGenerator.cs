using System;
using System.Collections;
using System.Collections.Generic;

public class WeatherGenerator : MonoSingleton<WeatherGenerator>
{
    // actually a state machine
    public bool Enabled { get; set; }
    public Weather Weather => _currentWeather;
    public ClearBehaviour ClearBehaviour;
    public WindyBehaviour WindyBehaviour;
    public bool IsCheckingWeather { get; set; }

    private Weather _currentWeather;

    private void Start()
    {
        ClearBehaviour.Hide();
        WindyBehaviour.Hide();
        ChangeWeather(new ClearWeather());
        IsCheckingWeather = false;
    }

    private void Update()
    {
        if(!Enabled) return;
        if (_currentWeather != null)
        {
            if(!IsCheckingWeather)
            {
                StartCoroutine(_currentWeather.CheckWeather());
            }
        }
    }

    public void ChangeWeather(Weather newWeather)
    {
        if (_currentWeather != null)
        {
            _currentWeather.ExitState();
        }

        _currentWeather = newWeather;

        if (_currentWeather != null)
        {
            _currentWeather.owner = this;
            _currentWeather.EnterState();
        }
    }
}