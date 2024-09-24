using JunEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MMSingleton<GameManager>
{
    [SerializeField] GameObject PlayerPivot;
    [SerializeField] GameObject Level;

    public void PlayGame()
    {
        PlayerPivot.SetActive(true);
        Level.SetActive(true);
    }
}
