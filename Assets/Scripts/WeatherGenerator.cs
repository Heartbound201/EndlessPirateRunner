using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class WeatherGenerator : MonoSingleton<WeatherGenerator>
{
    public bool Enabled { get; set; }
    public ObservableInt score;
    public List<SpawnData<Weather>> weatherSpawnData = new List<SpawnData<Weather>>();
    
    public float frequencyMin;
    public float frequencyMax;
    public float frequency;
    public float frequencyLinearChange;


    private bool _isChangingWeather;
    private bool _isWeatherActive;


    private void Update()
    {
        if(!Enabled) return;
        ChangeWeather();
    }
    
    public void ChangeWeather()
    {
        if(_isChangingWeather || _isWeatherActive) return;
        StartCoroutine(DoChangeWeather());
    }

    private IEnumerator DoChangeWeather()
    {
        _isChangingWeather = true;

        Weather randomWeather = PickRandomWeather(weatherSpawnData);
        if(randomWeather != null)
        {
            randomWeather.Show();
            _isWeatherActive = true;
            randomWeather.OnExpiration += () => { _isWeatherActive = false; };
        }

        // change frequency
        yield return new WaitForSeconds(ChangeByDistance(frequency, frequencyLinearChange, score.Value, frequencyMin, frequencyMax));
        _isChangingWeather = false;
    }

    private float ChangeByDistance(float value, float decrement, float dist, float min, float max)
    {
        return Mathf.Clamp(value + dist * decrement, min, max);
    }

    private Weather PickRandomWeather(List<SpawnData<Weather>> spawnData)
    {
        List<SpawnData<Weather>> prototypes = spawnData.Where(sd => score.Value >= sd.minScore).ToList();
        
        List<int> prob = new List<int>();
        for (int i = 0; i < prototypes.Count; i++)
        {
            for (int j = 0; j < prototypes[i].weight; j++)
            {
                prob.Add(i);
            }
        }

        if (prob.Count == 0) return null;
        return prototypes[prob[Random.Range(0, prob.Count)]].data;
    }
}
