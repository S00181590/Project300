using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSavingMover : MonoBehaviour
{
    bool sizeBool;
    public int smallSize = 210;

    void Update()
    {
        //if (gameObject.activeSelf == true)
        //    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(Mathf.PingPong(Time.time, 1f),
        //        transform.localScale.y, transform.localScale.z), Time.deltaTime * 5);

        if (gameObject.activeSelf == true)
        {
            if (GetComponent<RectTransform>().sizeDelta.x < 250)
            {
                GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(GetComponent<RectTransform>().sizeDelta,
                    new Vector2(GetComponent<RectTransform>().sizeDelta.x + 10, GetComponent<RectTransform>().sizeDelta.y), Time.deltaTime * 5);
            }

            if (GetComponent<RectTransform>().sizeDelta.x >= 250)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(smallSize, GetComponent<RectTransform>().sizeDelta.y);
            }
        }
    }
}
