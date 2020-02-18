using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSavingMover : MonoBehaviour
{
    bool sizeBool;
    public int smallSize = 210;
    public int bigSize = 260;

    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            if (GetComponent<RectTransform>().sizeDelta.x < bigSize)
            {
                GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(GetComponent<RectTransform>().sizeDelta,
                    new Vector2(GetComponent<RectTransform>().sizeDelta.x + 15, GetComponent<RectTransform>().sizeDelta.y), Time.deltaTime * 2);
            }

            if (GetComponent<RectTransform>().sizeDelta.x >= bigSize)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(smallSize, GetComponent<RectTransform>().sizeDelta.y);
            }
        }
    }
}
