using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [HideInInspector]
    public bool weaponBool = true;
    public GameObject sword;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponBool = !weaponBool;
        }

        if (weaponBool)
        {
            //Sword
            GetComponent<ArrowShooter>().enabled = false;
            sword.SetActive(true);
        }
        else
        {
            //Bow
            GetComponent<ArrowShooter>().enabled = true;
            sword.SetActive(false);
        }
    }
}
