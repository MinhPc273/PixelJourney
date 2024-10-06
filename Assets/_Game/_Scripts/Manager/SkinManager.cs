using JunEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SkinManager : MMSingleton<SkinManager>
{
    public PlayerSkin PlayerSkinData;

    [SerializeField] Animator _playerModel;


    override protected void Awake()
    {
        base.Awake();
        _playerModel.runtimeAnimatorController = PlayerSkinData.skins[Pref.IDSkin].Animator;
    }

    public void SetSkin(int id)
    {
        if(id == Pref.IDSkin)
            return;
        Pref.IDSkin = id;
        _playerModel.runtimeAnimatorController = PlayerSkinData.skins[id].Animator;
    }
}
