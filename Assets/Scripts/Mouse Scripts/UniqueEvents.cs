using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UniqueEvents : EventTrigger
{


    public override void OnPointerEnter(PointerEventData eventData)
    {
        MouseControl.mouseState.Clickable();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        MouseControl.mouseState.Default();
    }

}
