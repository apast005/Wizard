using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{

    public Texture2D clickableObjects, defaultState;
    public static MouseControl mouseState;


    private void OnApplicationFocus(bool applicationInFocus)
    {
        if (applicationInFocus == true)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Default();

        }
    }


    private void Awake()
    {
        if (mouseState == null)
        {
            mouseState = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Default();
    }

    public void Clickable()
    {
        Cursor.SetCursor(clickableObjects, Vector2.zero, CursorMode.Auto);
    }

    public void Default()
    {
        Cursor.SetCursor(defaultState, Vector2.zero, CursorMode.Auto);
    }

}
