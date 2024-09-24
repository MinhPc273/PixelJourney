using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButonControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected bool _isHover;

    public virtual bool IsSlected()
    {
        if(InputManager.Instance._InputType == InputType.Mobile)
        {
            return _isHover;
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHover = false;
    }
}
