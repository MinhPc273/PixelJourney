using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasWin : MonoBehaviour
{
    public GameObject ButtonNext;

    private void OnDisable()
    {
        ButtonNext.SetActive(true);
    }
}
