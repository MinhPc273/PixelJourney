using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenu;
    //public GameObject MainMenuBg;
    public GameObject GamePlay;
    public GameObject Shop;
    public GameObject Pause;
    public GameObject Win;
    public GameObject Lose;

    private void Awake()
    {
        MainMenu.SetActive(true);
        //MainMenuBg.SetActive(true);
        GamePlay.SetActive(false);
        Shop.SetActive(false);
        Pause.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Current.OnInitGame += PlayGame;
        GameManager.Current.OnWin += WinGame;
        GameManager.Current.OnLose += LoseGame;
    }


    public void ButtonPlay()
    {
        GameManager.Current.PlayGame();
    }

    public void PlayGame()
    {
        MainMenu.SetActive(false);
        GamePlay.SetActive(true);
        Lose.SetActive(false);
        Win.SetActive(false);
    }

    public void LoseGame()
    {
       Lose.SetActive(true);
    }

    public void WinGame()
    {
        Win.SetActive(true);
    }
}
