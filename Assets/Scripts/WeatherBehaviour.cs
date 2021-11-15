using UnityEngine;

public abstract class WeatherBehaviour : MonoBehaviour
{
    public float duration;
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}