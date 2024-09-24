using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnJump : ButonControl, IPointerDownHandler, IPointerUpHandler
{
    private bool _isPress;

    public override bool IsSlected()
    {
        if (InputManager.Instance._InputType == InputType.Mobile)
        {
            return _isHover && _isPress;
        }
        return false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPress = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPress = false;
    }
}
