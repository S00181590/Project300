using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public delegate void InventoryItemDelegate(int itemID);

public class GridButton : MonoBehaviour, IPointerEnterHandler
{
    public Image imgIcon;
    int itemID;

    public event InventoryItemDelegate Selected;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Selected != null)
            Selected(itemID);
    }

    public void InitializeGridButton(int itemID, string iconName)
    {
        imgIcon.sprite = Resources.Load<Sprite>(@"Assets/InventoryTxt" + iconName);
        this.itemID = itemID;
    }
}
