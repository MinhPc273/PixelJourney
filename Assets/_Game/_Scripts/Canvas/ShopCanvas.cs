using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtFruit;
    [SerializeField] BtnSkin _btnSkinPrefab;
    [SerializeField] Transform _btnSkinParent;

    [SerializeField] List<BtnSkin> _listBtnSkin;

    private BtnSkin _currentButton;

    private PlayerSkin _playerSkin;

    public GameObject Message;


    private void OnEnable()
    {
        Message.SetActive(false);

        if (_playerSkin == null)
        {
            _playerSkin = SkinManager.Instance.PlayerSkinData;
        }

        UpdateApple();

        foreach (var btnSkin in _listBtnSkin)
        {
            btnSkin.gameObject.SetActive(false);
        }

        for (int i = 0; i < _playerSkin.skins.Count; i++)
        {
            if (i >= _listBtnSkin.Count)
            {
                var btnSkin = Instantiate(_btnSkinPrefab, _btnSkinParent);
                btnSkin.name = _btnSkinPrefab.name + i;
                _listBtnSkin.Add(btnSkin);
            }

            _listBtnSkin[i].SetData(_playerSkin.skins[i]);
            _listBtnSkin[i].gameObject.SetActive(true);
        }

        foreach(var btnSkin in _listBtnSkin)
        {
            if(btnSkin.ID == Pref.IDSkin)
            {
                _currentButton = btnSkin;
            }

            btnSkin.OnUse = () =>
            {
                if (_currentButton != null)
                {
                    _currentButton.UnSlected();
                }

                _currentButton = btnSkin;
            };

            btnSkin.OnBuySuccess = () =>
            {
                UpdateApple();
            };

            btnSkin.OnBuyFall = () =>
            {
                Message.SetActive(true);
            };
        }
    }

    private void UpdateApple()
    {
        _txtFruit.text = Pref.Fruit.ToString();
    }
}
