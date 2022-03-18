using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : Interactable
{
    [SerializeField] public Transform teleportTarget;

    public override void Interact()
    {
        gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public override void Teleport()
    {
        Debug.Log("Action Done in Player Controller");
    }
}
