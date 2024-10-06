using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject ChooseLevel;
    public GameObject GamePlay;
    public GameObject Shop;
    public GameObject Pause;
    public GameObject Win;
    public GameObject Lose;

    private void Awake()
    {
        MainMenu.SetActive(true);
        GamePlay.SetActive(false);
        Shop.SetActive(false);
        Pause.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
    }

    private void Start()
    {
        GameManager.Current.OnInitGame += PlayGame;
        GameManager.Current.OnWin += WinGame;
        GameManager.Current.OnLose += LoseGame;
    }

    public void PlayGame()
    {
        MainMenu.SetActive(false);
        ChooseLevel.SetActive(false);
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
