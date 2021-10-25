using UnityEngine.Events;

public class GameOverView : View
{
    public UnityAction OnRetryClicked;
    public UnityAction OnShopClicked;
    
    public void RetryClick()
    {
        OnRetryClicked?.Invoke();
    }
    public void ShopClick()
    {
        OnShopClicked?.Invoke();
    }
}