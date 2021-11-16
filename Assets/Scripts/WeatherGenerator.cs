using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeatherGenerator : MonoSingleton<WeatherGenerator>
{
    // actually a state machine
    public bool Enabled { get; set; }
    public ObservableInt distance;
    public List<WeatherSpawnData> WeatherSpawnData = new List<WeatherSpawnData>();
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
    
    private WeatherBehaviour PickRandomWeather(List<WeatherSpawnData> spawnData)
    {
        List<WeatherSpawnData> prototypes = spawnData.Where(sd => distance.Value >= sd.minDistance).ToList();
        
        List<int> prob = new List<int>();
        for (int i = 0; i < prototypes.Count; i++)
        {
            for (int j = 0; j < prototypes[i].chance; j++)
            {
                prob.Add(i);
            }
        }
        return prototypes[prob[Random.Range(0, prob.Count)]].behaviour;
    }
}

public class WeatherSpawnData
{
    public WeatherBehaviour behaviour;
    public int minDistance;
    [Range(0f, 1f)]
    public float chance;
}