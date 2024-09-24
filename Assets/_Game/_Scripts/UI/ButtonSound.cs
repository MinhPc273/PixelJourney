using JunEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] Sprite[] _sprites;
    [SerializeField] Image _image;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        LoadSprites();
        _button.onClick.AddListener(() =>
        {
            AudioSystem.Instance.ButtonSound();
            LoadSprites();
        });
    }

    private void LoadSprites()
    {
        if (PlayerPrefsExtension.GetBool("isPlay", true))
        {
            _image.sprite = _sprites[0];
        }
        else
        {
            _image.sprite = _sprites[1];
        }
    }
}
