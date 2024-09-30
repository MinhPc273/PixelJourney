using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtHp;
    [SerializeField] TextMeshProUGUI _txtKey;

    private void OnEnable()
    {
        PlayerController.Current.OnTakeDame += UpdateHP;
        PlayerController.Current.OnTakeKey += UpdateKey;
    }

    private void OnDisable()
    {
        PlayerController.Current.OnTakeDame -= UpdateHP;
        PlayerController.Current.OnTakeKey -= UpdateKey;
    }

    private void Start()
    {
        UpdateHP();
        UpdateKey();
    }

    private void UpdateHP()
    {
        _txtHp.text = $"{PlayerController.Current.Hp} / {PlayerController.Current.HpMAX}";
    }

    private void UpdateKey()
    {
        _txtKey.text = $"{PlayerController.Current.Key} / {PlayerController.Current.MaxKey}";
    }
}
