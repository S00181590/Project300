using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    public bool isClimbing;
    // Start is called before the first frame update
    void Start()
    {
        isClimbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isClimbing == true)
        {
            float direction = Input.GetAxis("Vertical") * Time.deltaTime;

            transform.Translate(new Vector3(0, direction,0));

        }
    }
}
