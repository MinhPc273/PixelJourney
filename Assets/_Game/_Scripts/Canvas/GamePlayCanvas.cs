using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtHp;
    [SerializeField] TextMeshProUGUI _txtKey;
    [SerializeField] TextMeshProUGUI _txtFruit;

    private void OnEnable()
    {
        PlayerController.Current.OnTakeDame += UpdateHP;
        PlayerController.Current.OnTakeKey += UpdateKey;
        PlayerController.Current.OnTakeFruit += UpdateFruit;
    }

    private void OnDisable()
    {
        PlayerController.Current.OnTakeDame -= UpdateHP;
        PlayerController.Current.OnTakeKey -= UpdateKey;
        PlayerController.Current.OnTakeFruit -= UpdateFruit;
    }

    private void Start()
    {
        UpdateHP();
        UpdateKey();
        UpdateFruit();
    }

    public void UpdateFruit()
    {
        _txtFruit.text = PlayerPrefs.GetInt(KeyPref.FRUIT).ToString();
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
