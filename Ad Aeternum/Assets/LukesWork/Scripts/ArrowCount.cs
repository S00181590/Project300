using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCount : MonoBehaviour
{
    public Text arrowCountText;
    public int arrowCount;
    public DataHandler dataHandler;

    void Start()
    {
        dataHandler.OnSpawnLoad();
        arrowCount = dataHandler.data.arrowCount;
    }

    void Update()
    {
        arrowCountText.text = "x " + arrowCount.ToString();
    }
}
