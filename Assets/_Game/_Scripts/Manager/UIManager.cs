using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject MainMenuBg;
    public GameObject GamePlay;

    public void ButtonPlay()
    {
        MainMenu.SetActive(false);
        MainMenuBg.SetActive(false);
        GamePlay.SetActive(true);
        GameManager.Current.PlayGame();
    }
}
