using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsPanel : MonoBehaviour
{
    public Text txtName;
    public Text txtDescription;
    public Text txtValue;
    public Image imgIcon;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetItem(int itemID)
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);

        Item item = GameManager.FindInventoryItemByID(itemID);

        if(item != null)
        {
            imgIcon.sprite = Resources.Load<Sprite>("InventoryIcons/" + item.IconName);
            txtName.text = item.Name;
            txtDescription.text = item.Description;
            txtValue.text = "€" + item.Value;
        }
    }
}
