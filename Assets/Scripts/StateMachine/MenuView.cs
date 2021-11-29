using UnityEngine;
using UnityEngine.Events;

public class MenuView : View
{
    public UnityAction OnStartClicked;
    public UnityAction OnShopClicked;
    public UnityAction OnQuitClicked;
    public UnityAction OnSettingsClicked;

    public Vector3 cameraPosition;
    public Quaternion cameraRotation;

    public void MoveCamera()
    {
        Camera.main.transform.position = cameraPosition;
        Camera.main.transform.rotation = cameraRotation;
        
    }
    
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