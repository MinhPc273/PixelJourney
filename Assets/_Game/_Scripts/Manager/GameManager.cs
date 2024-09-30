using JunEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MMSingleton<GameManager>
{
    [SerializeField] GameObject PlayerPivot;
    [SerializeField] GameObject Level;

    public Action OnInitGame;
    public Action OnWin;
    public Action OnLose;

    override protected void Awake()
    {
        base.Awake();
        PlayerPivot.SetActive(false);
        Level.SetActive(false);
    }

    public void PlayGame()
    {
        PlayerPivot.SetActive(true);
        Level.SetActive(true);
        OnInitGame?.Invoke();
    }

    public void Lose()
    {
        OnLose?.Invoke();
    }

    public void Win()
    {
        OnWin?.Invoke();
    }
}
