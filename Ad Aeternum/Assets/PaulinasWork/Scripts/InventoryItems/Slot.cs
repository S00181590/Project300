﻿using System.Collections;
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

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        DisplayItem();
    }

    private void Start()
    {
        slotIcon = transform.GetChild(0);
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
        Debug.Log(item.GetComponent<Item>().description);
    }
}