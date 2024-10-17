using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasWin : MonoBehaviour
{
    public TextMeshProUGUI TxtFruit;
    public GameObject ButtonNext;
    public GameObject ButtonCommingSoon;


    private void OnDisable()
    {
        ButtonNext.SetActive(true);
        ButtonCommingSoon.SetActive(false);
    }
}
