using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameView : View
{
    public UnityAction OnPauseClicked;
    public UnityAction OnFinishClicked;

    public PlayerData PlayerData;
    public Text GoldText;
    public Text ScoreText;
    public Transform LivesParent;
    public GameObject LifePrefab;

    public Vector3 cameraPosition;
    public Quaternion cameraRotation;

    private List<GameObject> _lives = new List<GameObject>();
    private Color _lifePrefabOriginalColor;

    public void MoveCamera()
    {
        Camera.main.transform.position = cameraPosition;
        Camera.main.transform.rotation = cameraRotation;
        
    }

    private void Start()
    {
        PlayerData.gold.OnChange += UpdateGold;
        PlayerData.score.OnChange += UpdateScore;
        PlayerData.lives.OnChange += UpdateLives;

        _lifePrefabOriginalColor = LifePrefab.GetComponent<Image>().color;
    }
    private void OnDestroy()
    {
        PlayerData.gold.OnChange -= UpdateGold;
        PlayerData.score.OnChange -= UpdateScore;
        PlayerData.lives.OnChange -= UpdateLives;
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
        GoldText.text = PlayerData.gold.Value.ToString();
    }
    public void UpdateScore()
    {
        ScoreText.text = PlayerData.score.Value.ToString();
    }
    public void UpdateLives()
    {
        while (_lives.Count < PlayerData.currentShip.lives)
        {
            GameObject go = Instantiate(LifePrefab, LivesParent);
            _lives.Add(go);
        }

        while (_lives.Count > PlayerData.currentShip.lives)
        {
            GameObject o = _lives[0];
            Destroy(o);
            _lives.Remove(o);
        }
        
        for (int i = 0; i < _lives.Count; i++)
        {
            if (i < PlayerData.lives.Value)
            {
                _lives[i].GetComponent<Image>().color = _lifePrefabOriginalColor;
            }
            else
            {
                _lives[i].GetComponent<Image>().color = Color.gray;
            }
        }
    }
}