using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnLevel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtLevel;
    [SerializeField] GameObject _lock;
    [SerializeField] GameObject _comingSoon;

    public Action OnClick;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => OnClick?.Invoke());
    }

    private void DisableAll()
    {
        _txtLevel.gameObject.SetActive(false);
        _lock.SetActive(false);
        _comingSoon.SetActive(false);
    }

    public void SetLevel(int level)
    {
        DisableAll();
        _txtLevel.text = level.ToString();  
        _txtLevel.gameObject.SetActive(true);
    }

    public void Lock()
    {
        DisableAll();
        _lock.SetActive(true);
    }

    public void ComingSoon()
    {
        DisableAll();
        _comingSoon.SetActive(true);
    }
}
