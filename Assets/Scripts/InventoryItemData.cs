using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData : MonoBehaviour
{
    public bool inventoryEnabled = false;
    public GameObject inventory;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;
    public GameObject slotHolder;


    void Start()
    {
        allSlots = 35;
        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
        }

    }

}
