using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [HideInInspector]
    public bool weaponBool = true;
    public GameObject sword, bow;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            weaponBool = !weaponBool;
        }

        if (weaponBool)
        {
            //Sword
            GetComponent<ArrowShooter>().enabled = false;
            sword.SetActive(true);
            bow.SetActive(false);
        }
        else
        {
            //Bow
            GetComponent<ArrowShooter>().enabled = true;
            sword.SetActive(false);
            bow.SetActive(true);
        }
    }
}
