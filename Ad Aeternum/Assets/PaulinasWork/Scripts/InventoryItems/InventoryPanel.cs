using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;


public class InventoryPanel : MonoBehaviour
{

    public RectTransform lstGridInventory;
    public GameObject GridButtonPrefab;
    public ItemDetailsPanel detailsPanel;

    void Start()
    {
        LoadList(GameManager.MasterCollection);
    }

    public void LoadList(List<Item> items)
    {

        for(int i =0; i < items.Count;i++)
        {
            GameObject button = Instantiate(GridButtonPrefab, lstGridInventory);

            GridButton gridButton = button.GetComponent<GridButton>();
            gridButton.InitializeGridButton(items[i].ID, items[i].IconName);

            gridButton.Selected += GridButton_OnSelected;
        }
    }

    private void GridButton_OnSelected(int itemID)
    {
        detailsPanel.SetItem(itemID);
    }
}
