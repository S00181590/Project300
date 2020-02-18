using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorChanger : MonoBehaviour
{
    Image image;
    public Color color1 = Color.white, color2 = new Color(0.4f, 0, 0);
    public float changeSpeed = 2;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }
    
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            image.color = Color.Lerp(color1, color2, Time.deltaTime * changeSpeed);
        }
    }
}
