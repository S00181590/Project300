using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : Player
{
    bool isslowed = false;


    public  void OnTriggerEnter(Collider collision)
    {
        if(collision.name == "Player")
        {
            isslowed = true;
            

        }
    }
}
