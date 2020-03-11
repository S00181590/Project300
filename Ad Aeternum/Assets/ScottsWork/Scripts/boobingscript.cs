using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boobingscript : MonoBehaviour
{
    Vector3 bobingpos1, bobingpos2, offset;
    float bobbingspeed = 0.5f;
    Vector3  moveTo;
    // Start is called before the first frame update
    void Start()
    {
        offset = Vector3.down;
        bobingpos1 = transform.position;
        bobingpos2 = transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.position ==bobingpos1)
        {
            moveTo = bobingpos2;
        }
        if (transform.position == bobingpos2)
        {
            moveTo = bobingpos1;
        }
        transform.position = Vector3.MoveTowards(transform.position, moveTo, bobbingspeed);
    }
}
