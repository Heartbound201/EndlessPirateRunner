using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameView : View
{
    public UnityAction OnPauseClicked;
    public UnityAction OnFinishClicked;

    public ObservableInt Gold;
    public Text GoldText;
    public ObservableInt Distance;
    public Text DistanceText;

    private void Start()
    {
        Gold.OnChange += UpdateGold;
        Distance.OnChange += UpdateDistance;
    }
    private void OnDestroy()
    {
        Gold.OnChange -= UpdateGold;
        Distance.OnChange -= UpdateDistance;
    }

    public void PauseClick()
    {
        OnPauseClicked?.Invoke();
    }

    public void FinishClick()
    {
        OnFinishClicked?.Invoke();
    }
    
    private void UpdateGold()
    {
        GoldText.text = Gold.Value.ToString();
    }
    private void UpdateDistance()
    {
        DistanceText.text = Distance.Value.ToString();
    }
}