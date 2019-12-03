using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridButton : MonoBehaviour
{ 
    public Image imgIcon;
    public Text txtName;
    public int ItemID;

    public void SetItem(int itemID)
    {
        Item foundItem = GameManager.Instance.AllItemsInTheGame.GetItem(itemID);

        txtName.text = foundItem.Name;
        imgIcon.sprite = foundItem.Icon;
        imgIcon.color = foundItem.Tint;

        Debug.Log(foundItem.Tint);
       
        ItemID = itemID;        
    }
}
