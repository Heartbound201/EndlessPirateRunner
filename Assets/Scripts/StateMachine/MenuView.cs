using UnityEngine;
using UnityEngine.Events;

public class MenuView : View
{
    public UnityAction OnStartClicked;
    public UnityAction OnShopClicked;

    public void StartClick()
    {
        OnStartClicked?.Invoke();
    }
    public void ShopClick()
    {
        OnShopClicked?.Invoke();
    }
}