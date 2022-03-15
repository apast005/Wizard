using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class ClickableObjects : MonoBehaviour
{

    public void OnMouseEnter()
    {
        MouseControl.mouseState.Clickable();
    }

    public void OnMouseExit()
    {
        MouseControl.mouseState.Default();
    }

}
