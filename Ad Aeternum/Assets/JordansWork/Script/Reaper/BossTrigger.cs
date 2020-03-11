using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        //foreach(GameObject r in lay)
        boss = GameObject.Find("ReaperBoss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            boss.GetComponent<ReaperMvementController>().PlayerIsHere = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            boss.GetComponent<ReaperMvementController>().PlayerIsHere = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            boss.GetComponent<ReaperMvementController>().PlayerIsHere = false;
        }
    }
}
