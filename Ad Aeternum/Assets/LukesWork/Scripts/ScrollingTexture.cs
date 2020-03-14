using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    //Scrolls the entire texture on an object, such as a water effect

    public float ScrollX = 0.5f, ScrollY = 0.5f;
    float OffsetX, OffsetY;
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        OffsetX = Time.time * ScrollX;
        OffsetY = Time.time * ScrollY;

        //rend.material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
        GetComponent<Material>().mainTextureOffset = new Vector2(OffsetX, OffsetY); 
    }
}
