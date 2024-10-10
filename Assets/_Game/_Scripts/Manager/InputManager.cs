using JunEngine;

public class InputManager : MMSingleton<InputManager>
{
    public InputType _InputType;
    public ButonControl BtnLeft;
    public ButonControl BtnRight;
    public BtnJump BtnJump;

    protected override void Awake()
    {
        base.Awake();
        _InputType = InputType.Mobile;

#if UNITY_EDITOR
        _InputType = InputType.Keyboard;
#endif
    }
}
public enum InputType
{
    Keyboard,
    Mobile
}
