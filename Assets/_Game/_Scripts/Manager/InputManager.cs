using JunEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MMSingleton<InputManager>
{
    public InputType _InputType;
    public ButonControl BtnLeft;
    public ButonControl BtnRight;
    public BtnJump BtnJump;
}

public enum InputType
{
    Keyboard,
    Mobile
}
