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
        arrowCount = 10;
    }

    void Update()
    {
        arrowCountText.text = "Arrows: " + arrowCount.ToString();
    }
}
