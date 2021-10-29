using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverView : View
{
    public UnityAction OnRetryClicked;
    public UnityAction OnShopClicked;
    
    public ObservableInt Gold;
    public Text GoldText;
    public ObservableInt Distance;
    public Text DistanceText;
    public Text NewHighScore;
    
    public void RetryClick()
    {
        OnRetryClicked?.Invoke();
    }
    public void ShopClick()
    {
        OnShopClicked?.Invoke();
    }

    public void FillRecap()
    {
        NewHighScore.gameObject.SetActive(false);
        GoldText.text = Gold.Value.ToString();
        DistanceText.text = Distance.Value.ToString();
        if (IsNewHighScore(Distance.Value))
        {
            NewHighScore.gameObject.SetActive(true);
        }
    }

    private bool IsNewHighScore(int value)
    {
        return true;
    }
}