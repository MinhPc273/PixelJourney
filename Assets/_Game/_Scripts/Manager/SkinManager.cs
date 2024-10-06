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
    }

    public void SetSkin(int id)
    {
        if(id == Pref.IDSkin)
            return;
        _playerModel.runtimeAnimatorController = PlayerSkinData.skins[id].Animator;
    }
}
