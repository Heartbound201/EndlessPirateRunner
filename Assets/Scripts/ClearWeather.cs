using System.Collections;
using UnityEngine;

public class ClearWeather : Weather
{
    private float chance;
    public override void ExitState()
    {
        owner.ClearBehaviour.Hide();
    }

    public override IEnumerator CheckWeather()
    {
        owner.IsCheckingWeather = true;
        if (Random.Range(0f, 1f) < .3f)
        {
            owner.ChangeWeather(new WindyWeather());
        }

        yield return new WaitForSeconds(1);
        owner.IsCheckingWeather = false;
    }

    public override void EnterState()
    {
        owner.ClearBehaviour.Show();
    }
}