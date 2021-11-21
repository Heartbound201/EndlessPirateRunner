using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameView : View
{
    public UnityAction OnPauseClicked;
    public UnityAction OnFinishClicked;

    public Player Player;
    public ObservableInt Gold;
    public Text GoldText;
    public ObservableInt Distance;
    public Text DistanceText;
    public ObservableInt Lives;
    public Transform LivesParent;
    public GameObject LifePrefab;

    private List<GameObject> _lives = new List<GameObject>();

    private void Start()
    {
        Gold.OnChange += UpdateGold;
        Distance.OnChange += UpdateDistance;
        Lives.OnChange += UpdateLives;
    }
    private void OnDestroy()
    {
        Gold.OnChange -= UpdateGold;
        Distance.OnChange -= UpdateDistance;
        Lives.OnChange -= UpdateLives;
    }
    
    

    public void PauseClick()
    {
        OnPauseClicked?.Invoke();
    }

    public void FinishClick()
    {
        OnFinishClicked?.Invoke();
    }
    
    public void UpdateGold()
    {
        GoldText.text = Gold.Value.ToString();
    }
    public void UpdateDistance()
    {
        DistanceText.text = Distance.Value.ToString();
    }
    public void UpdateLives()
    {
        while (_lives.Count < Player.playerShip.maxLives)
        {
            GameObject go = Instantiate(LifePrefab, LivesParent);
            _lives.Add(go);
        }

        while (_lives.Count > Player.playerShip.maxLives)
        {
            GameObject o = _lives[0];
            Destroy(o);
            _lives.Remove(o);
        }
        
        for (int i = 0; i < _lives.Count; i++)
        {
            if (i < Player.playerShip.lives.Value)
            {
                _lives[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                _lives[i].GetComponent<Image>().color = Color.gray;
            }
        }
    }
}