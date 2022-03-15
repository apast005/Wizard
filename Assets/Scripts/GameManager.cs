using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Player Var/Obj
    GameObject player;
    playerHandler playerHandler;
    public float gravity = -9.8f;

    // Inventory Var/Obj
    [SerializeField] InventoryItemData inventoryItemData;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHandler = player.GetComponent<playerHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        inventoryItemData.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        // Keep cursor Visible
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        // Inventory Control
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryItemData.inventoryEnabled = !inventoryItemData.inventoryEnabled;
        }
        if (inventoryItemData.inventoryEnabled == true)
        {
            inventoryItemData.inventory.SetActive(true);
        }
        else
        {
            inventoryItemData.inventory.SetActive(false);
        }

    }
}
