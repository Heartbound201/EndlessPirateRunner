using UnityEngine;
using UnityEngine.Events;

public abstract class Weather : MonoBehaviour
{
    public UnityAction OnExpiration;
    public Player player;
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