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
            if (i >= _listBtnLevel.Count)
            {
                BtnLevel btnLevel = Instantiate(_btnLevelPrefab, _content);
                btnLevel.name = _btnLevelPrefab.name + i;
                _listBtnLevel.Add(btnLevel);
            }

            _listBtnLevel[i].gameObject.SetActive(true);
        }


        int index = 0;
        foreach (var item in _listBtnLevel)
        {
            if (index <= Pref.LevelUnlocked)
            {
                item.SetLevel(index + 1);

                int levelIndex = index;

                item.OnClick = () =>
                {
                    GameManager.Instance.LoadLevel(levelIndex);
                    GameManager.Instance.PlayGame();
                };
            }
            else
            {
                item.Lock();
            }
            index++;
        }
    }

    private void Start()
    {
        BtnLevel btnLevelComming = Instantiate(_btnLevelPrefab, _content);
        btnLevelComming.name = _btnLevelPrefab.name + "Coming";
        btnLevelComming.ComingSoon();
        btnLevelComming.transform.SetAsLastSibling();
    }
}
