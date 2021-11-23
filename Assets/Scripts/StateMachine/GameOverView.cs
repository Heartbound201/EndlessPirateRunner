using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameOverView : View
{
    public UnityAction OnRetryClicked;
    public UnityAction OnMenuClicked;
    
    
    public PlayerData PlayerData;
    public Text GoldText;
    public Text ScoreText;
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
        GoldText.text = PlayerData.gold.Value.ToString();
        ScoreText.text = PlayerData.score.Value.ToString();
        if (IsNewHighScore(PlayerData.highscore, PlayerData.score.Value))
        {
            PlayerData.highscore = PlayerData.score.Value;
            NewHighScore.gameObject.SetActive(true);
        }
    }

    private bool IsNewHighScore(int oldScore, int newScore)
    {
        return oldScore < newScore;
    }
}