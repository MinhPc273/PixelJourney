using JunEngine;
using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkin", menuName = "ScriptableObjects/Player", order = 1)]
public class PlayerSkin : ScriptableObject
{
    public List<Skin> skins;
}

[Serializable]
public class Skin
{
    public int ID;
    public string Name;
    public Sprite Avatar;
    public AnimatorController Animator;
    public int Price;
    [SerializeField] bool _isBought;
    public bool IsBought
    {
        get => PlayerPrefsExtension.GetBool(KeyPref.BUY_SKIN + ID, _isBought);
        set => PlayerPrefsExtension.SetBool(KeyPref.BUY_SKIN + ID, value);
    }
}
