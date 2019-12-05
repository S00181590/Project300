using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;


public class InventoryPanel : MonoBehaviour
{

    public Transform Content;
    public GameObject GridButtonPrefab;

    public void SetInventory(List<int> items)
    {

        GameObject button;
        foreach(int id in items)
        {
            button = Instantiate(GridButtonPrefab, Content);
            button.GetComponent<GridButton>().SetItem(id);
        }

    }
}
