using System.Collections;
using UnityEngine;

public class WindyWeather : Weather
{

    private float time;
    public override void EnterState()
    {
        owner.WindyBehaviour.Show();
    }
    
    public override IEnumerator CheckWeather()
    {
        owner.IsCheckingWeather = true;
        yield return new WaitForSeconds(2);
        owner.ChangeWeather(new ClearWeather());
        owner.IsCheckingWeather = false;
    }
    public override void ExitState()
    {
        owner.WindyBehaviour.Hide();
    }
}