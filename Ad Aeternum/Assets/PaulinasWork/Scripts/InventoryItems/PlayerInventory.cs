using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public SavableInventory Inventory = new SavableInventory();

    public InventoryPanel TestDisplay;

    private void Start()
    {
        Inventory.Load("PlayerInventory");

        if (Inventory.Items.Count > 0)
            TestDisplay.SetInventory(Inventory.Items);
    }
    
}
