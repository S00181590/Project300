using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCount : MonoBehaviour
{
    public Text arrowCountText;
    public int arrowCount;

    void Start()
    {
        arrowCount = 30;
    }

    void Update()
    {
        arrowCountText.text = "x " + arrowCount.ToString();
    }
}
