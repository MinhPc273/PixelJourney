using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pref
{
    public static int Fruit
    {
        get => PlayerPrefs.GetInt(KeyPref.FRUIT, 0);
        set => PlayerPrefs.SetInt(KeyPref.FRUIT, value);
    }   

    public static int LevelUnlocked
    {
        get => PlayerPrefs.GetInt(KeyPref.LEVEL, 0);
        set => PlayerPrefs.SetInt(KeyPref.LEVEL, value);
    }

    public static int IDSkin
    {
        get => PlayerPrefs.GetInt(KeyPref.ID_SKIN, 0);
        set => PlayerPrefs.SetInt(KeyPref.ID_SKIN, value);
    }
}
