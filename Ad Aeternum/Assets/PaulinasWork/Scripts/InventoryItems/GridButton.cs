using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridButton : MonoBehaviour
{ 
    public Image imgIcon;
    public Text txtName;
    public int itemID;

    public void SetItem(int itemID)
    {
        Item foundItem = GameManager.Instance.AllItemsInTheGame.GetItem(itemID);

        imgIcon.sprite = foundItem.Icon;
        imgIcon.color = foundItem.Tint;
        txtName.text = foundItem.Name;

        itemID = itemID;
        
    }
}
