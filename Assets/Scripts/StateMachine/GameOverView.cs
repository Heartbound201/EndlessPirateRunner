using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverView : View
{
    public UnityAction OnRetryClicked;
    public UnityAction OnMenuClicked;
    
    
    public PlayerData playerData;
    public ObservableInt Distance;
    public Text GoldText;
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
        GoldText.text = playerData.gold.Value.ToString();
        DistanceText.text = Distance.Value.ToString();
        if (IsNewHighScore(playerData.highscore, Distance.Value))
        {
            playerData.highscore = Distance.Value;
            NewHighScore.gameObject.SetActive(true);
        }
    }

    private bool IsNewHighScore(int oldScore, int newScore)
    {
        return oldScore < newScore;
    }
}