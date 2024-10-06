using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosLevelCanvas : MonoBehaviour
{
    [SerializeField] BtnLevel _btnLevelPrefab;
    [SerializeField] Transform _content;
    [SerializeField] List<BtnLevel> _listBtnLevel;

    private void OnEnable()
    {
        foreach (var item in _listBtnLevel)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < GameManager.Instance.Levels.Length; i++)
        {
            if (_listBtnLevel.Count != 0 && i < _listBtnLevel.Count)
            {
                if (i <= Pref.LevelUnlocked)
                {
                    _listBtnLevel[i].SetLevel(i + 1);
                    _listBtnLevel[i].OnClick = () =>
                    {
                        GameManager.Instance.LoadLevel(i-1);
                        GameManager.Instance.PlayGame();
                        
                    };
                }
                else
                {
                    _listBtnLevel[i].Lock();
                }
                _listBtnLevel[i].gameObject.SetActive(true);
            }
            else
            {
                BtnLevel btnLevel = Instantiate(_btnLevelPrefab, _content);
                btnLevel.name = _btnLevelPrefab.name + "_" + (i+1);
                _listBtnLevel.Add(btnLevel);
                if (i <= Pref.LevelUnlocked)
                {
                    _listBtnLevel[i].SetLevel(i+1);
                    _listBtnLevel[i].OnClick = () =>
                    {
                        GameManager.Instance.LoadLevel(i-1);
                        GameManager.Instance.PlayGame();
                    };
                }
                else
                {
                    _listBtnLevel[i].Lock();
                }
                _listBtnLevel[i].gameObject.SetActive(true);
            }
        }

        BtnLevel btnLevelComming = Instantiate(_btnLevelPrefab, _content);
        _listBtnLevel.Add(btnLevelComming);
        btnLevelComming.name = _btnLevelPrefab.name + "Coming";
        btnLevelComming.ComingSoon();
    }
}
