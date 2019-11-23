using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOn : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        canvas.enabled = false;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            canvas.enabled = !canvas.enabled;       
        }
        
    }
}
