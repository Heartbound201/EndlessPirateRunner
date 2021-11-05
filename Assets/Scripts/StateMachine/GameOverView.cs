using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverView : View
{
    public UnityAction OnRetryClicked;
    public UnityAction OnMenuClicked;
    
    public ObservableInt Gold;
    public Text GoldText;
    public ObservableInt Distance;
    public Text DistanceText;
    public Text NewHighScore;
    
    public void RetryClick()
    {
        OnRetryClicked?.Invoke();
    }
    public void MenuClick()
    {
        OnMenuClicked?.Invoke();
    }

    public void FillRecap(Player player)
    {
        NewHighScore.gameObject.SetActive(false);
        GoldText.text = Gold.Value.ToString();
        DistanceText.text = Distance.Value.ToString();
        if (IsNewHighScore(player.HighScore, Distance.Value))
        {
            NewHighScore.gameObject.SetActive(true);
        }
    }

    private bool IsNewHighScore(int oldScore, int newScore)
    {
        return oldScore < newScore;
    }
}