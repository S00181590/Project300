using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    GameObject playerObj;

    void Start()
    {
        playerObj = GameObject.Find("PlayerMoveController");
        gameObject.transform.localPosition = new Vector3(-310, 0, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (playerObj.GetComponent<WeaponSwitch>().weaponBool == false)
            {
                //gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, 
                //    cam.WorldToScreenPoint(player.transform.position) + new Vector3(220, -120, 0), Time.deltaTime * 10);

                gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * 5);
            }
        }
        else
        {
            //gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition,
            //    new Vector3(2000, 135, 0), Time.deltaTime * 20);

            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, new Vector3(-320, 0, 0), Time.deltaTime * 5);
        }
    }
}
