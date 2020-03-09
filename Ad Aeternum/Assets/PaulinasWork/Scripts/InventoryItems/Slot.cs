using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;
    public int ID;
    public string type;
    public string description;
    public bool empty;

    public Transform slotIcon;
    public Sprite icon;

    public GameObject itemDisplayer;

    public GameObject hoverSprite;
    Text hoverName;
    Text hoverDescription;

    InventoryOn inventoryOn;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        DisplayItem();
    }

    private void Start()
    {
        hoverName = GameObject.Find("itemName").GetComponent<Text>();
        hoverSprite = GameObject.Find("itemIcon");
        hoverDescription = GameObject.Find("itemStats").GetComponent<Text>();
        slotIcon = transform.GetChild(0);
        hoverSprite.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        inventoryOn = GameObject.Find("InventorySphere").GetComponent<InventoryOn>();
    }

    void Update()
    {
        if (inventoryOn.isOpen == false)
        {
            hoverName.text = "";
            hoverDescription.text = "";
            hoverSprite.GetComponent<Image>().sprite = null;
            hoverSprite.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }

    public void UpdateSlot()
    {
        slotIcon.GetComponent<Image>().sprite = icon;
    }

    public void UseItem()
    {
        item.GetComponent<Item>().ItemUsage();
    }

    public void DisplayItem()
    {
        hoverName.text = item.GetComponent<Item>().name.ToString();
        hoverDescription.text = "Description: " + item.GetComponent<Item>().description.ToString();
        hoverSprite.GetComponent<Image>().sprite = item.GetComponent<Item>().Icon;
        hoverSprite.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
}