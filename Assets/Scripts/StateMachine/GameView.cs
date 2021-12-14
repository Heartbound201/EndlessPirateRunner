using System;
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

    public string poolKey;
    public GameObject LifePrefab;

    public Vector3 cameraPosition;
    public Quaternion cameraRotation;

    private List<GameObject> _lives = new List<GameObject>();
    private Color _lifePrefabOriginalColor;

    public void MoveCamera(Vector3 playerPos)
    {
        Camera.main.transform.position = playerPos + cameraPosition;
        Camera.main.transform.rotation = cameraRotation;
        
    }

    private void Awake()
    {
        if (GameObjectPoolController.AddEntry(poolKey, LifePrefab, 5, 10))
            Debug.Log("Pre-populating pool. key:" + poolKey);
        else
            Debug.Log(poolKey + "Pool already configured");
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
            Poolable obj = GameObjectPoolController.Dequeue(poolKey);
            obj.transform.SetParent(LivesParent);
            obj.gameObject.SetActive(true);
            obj.transform.localScale = Vector3.one;
            _lives.Add(obj.gameObject);
        }

        while (_lives.Count > PlayerData.currentShip.lives)
        {
            GameObject o = _lives[0];
            GameObjectPoolController.Enqueue(o.GetComponent<Poolable>());
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