using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCount : MonoBehaviour
{
    Text arrowCountText;
    public int arrowCount;
    DataHandler dataHandler;

    void Start()
    {
        arrowCountText = GameObject.Find("ArrowCountText").GetComponent<Text>();
        dataHandler = GameObject.Find("DataHandler").GetComponent<DataHandler>();

        arrowCount = dataHandler.data.arrowCount;
        dataHandler.OnSpawnLoad();
    }

    void Update()
    {
        arrowCountText.text = "x " + arrowCount.ToString();

        if (Input.GetKeyDown(KeyCode.M))
        {
            arrowCount += 1000;
        }
    }
}
