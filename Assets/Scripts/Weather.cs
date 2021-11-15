using System.Collections;

public abstract class Weather
{
    public WeatherGenerator owner;

    public abstract IEnumerator CheckWeather();

    public abstract void ExitState();

    public abstract void EnterState();
}