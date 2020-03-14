using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.x + 200, player.transform.position.z);
    }
}
