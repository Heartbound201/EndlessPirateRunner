using UnityEngine.Events;

public class PauseView : View
{
    public UnityAction OnResumeClicked;
    public UnityAction OnShopClicked;
    
    public void ResumeClick()
    {
        OnResumeClicked?.Invoke();
    }
    public void ShopClick()
    {
        OnShopClicked?.Invoke();
    }
}