using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnSkin : MonoBehaviour
{
    private int _id;
    public int ID => _id;
    [SerializeField] TextMeshProUGUI _txtName;
    [SerializeField] Image _avatar;
    [SerializeField] TextMeshProUGUI _txtPrice;
    [SerializeField] GameObject _btnBuy;
    [SerializeField] GameObject _btnUse;
    [SerializeField] GameObject _btnSelect;

    public Action OnBuySuccess;
    public Action OnBuyFall;
    public Action OnUse;

    private Skin _skin;

    public void SetData(Skin skin)
    {
        _skin = skin;
        _id = skin.ID;
        _txtName.text = skin.Name;
        _avatar.sprite = skin.Avatar;
        _txtPrice.text = skin.Price.ToString();

        _btnBuy.SetActive(false);
        _btnUse.SetActive(false);
        _btnSelect.SetActive(false);

        _btnBuy.SetActive(true);

        if(skin.IsBought)
        {
            _btnBuy.SetActive(false);
            _btnUse.SetActive(true);
        }

        if(skin.ID == Pref.IDSkin)
        {
            _btnUse.SetActive(false);
            _btnSelect.SetActive(true);
        }
    }


    public void ButtonUse()
    {
        SkinManager.Instance.SetSkin(_id);
        _btnUse.SetActive(false);
        _btnSelect.SetActive(true);
        OnUse?.Invoke();
    }

    public void UnSlected()
    {
        _btnSelect.SetActive(false);
        _btnUse.SetActive(true);
    }

    public void ButtonBuy()
    {
        if(Pref.Fruit >= int.Parse(_txtPrice.text))
        {
            Pref.Fruit -= int.Parse(_txtPrice.text);
            _txtPrice.gameObject.SetActive(false);
            _btnUse.SetActive(true);
            _skin.IsBought = true;
            OnBuySuccess?.Invoke();
        }
        else
        {
            OnBuyFall?.Invoke();
        }
    }
}
