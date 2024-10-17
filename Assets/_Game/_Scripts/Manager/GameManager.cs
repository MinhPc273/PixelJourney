using Cinemachine;
using JunEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MMSingleton<GameManager>
{
    [SerializeField] GameObject PlayerPivot;
    [SerializeField] CinemachineConfiner2D  Confiner;

    [SerializeField] Level[] _levels;

    public Level[] Levels
    {
        get => _levels;
    }

    [SerializeField] Transform _levelParent;

    private Level _currentLevel;

    private int _currentLevelIndex = 0;

    public Action OnInitGame;
    public Action OnWin;
    public Action OnLose;

    [SerializeField] CanvasWin _canvasWin;

    override protected void Awake()
    {
        Application.targetFrameRate = 144;
        base.Awake();
        Init();
        PlayerPivot.SetActive(false);
    }

    private void Init()
    {
        _levels = Resources.LoadAll<Level>("Level");
    }

    public void PlayGame()
    {
        Confiner.m_BoundingShape2D = _currentLevel.Bound.GetComponent<PolygonCollider2D>();
        PlayerPivot.transform.position = _levels[_currentLevelIndex].StartPos.position;
        PlayerPivot.SetActive(true);
        OnInitGame?.Invoke();
    }

    public void LoadLevel(int level)
    {
        if(_currentLevel != null) 
            Destroy(_currentLevel.gameObject);
        _currentLevelIndex = level;
        _currentLevel = Instantiate(_levels[level], _levelParent);
    }

    public void Lose()
    {
        OnLose?.Invoke();
    }

    public void Win()
    {
        _currentLevelIndex++;
        if(_currentLevelIndex >= _levels.Length)
        {
            _canvasWin.ButtonNext.SetActive(false);
            _canvasWin.ButtonCommingSoon.SetActive(true);
        }
        _canvasWin.TxtFruit.text = $"+ {PlayerController.Current.Fruit.ToString()}";
        if(_currentLevelIndex > Pref.LevelUnlocked)
        {
            Pref.LevelUnlocked = _currentLevelIndex;
        }
        OnWin?.Invoke();
    }

    public void OnMainMenu()
    {
        PlayerPivot.SetActive(false);
        if(_currentLevel != null)
            Destroy(_currentLevel.gameObject);
    }

    public void NewGame()
    {
        LoadLevel(_currentLevelIndex);
        PlayGame();
    }
}
