using UnityEngine;
using UnityEngine.Events;

public class MenuView : View
{
    public UnityAction OnStartClicked;
    public UnityAction OnShopClicked;
    public UnityAction OnQuitClicked;
    public UnityAction OnSettingsClicked;

    public void StartClick()
    {
        OnStartClicked?.Invoke();
    }
    public void ShopClick()
    {
        OnShopClicked?.Invoke();
    }
    public void QuitClick()
    {
        OnQuitClicked?.Invoke();
    }
    public void SettingsClick()
    {
        OnSettingsClicked?.Invoke();
    }
}