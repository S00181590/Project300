using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    public string type;
    public string description;
    public Sprite Icon;
    public bool pickedUp;
    public bool equipped;

    public bool playersWeapon;
    public GameObject weapon;
    public GameObject weaponManager;

    public void Start()
    {
        weaponManager = GameObject.FindWithTag("WeaponManager");
        if (!playersWeapon)
        {
            int allWeapons = weaponManager.transform.childCount;
            for (int i = 0; i < allWeapons; i++)
            {
                if(weaponManager.transform.GetChild(i).gameObject.GetComponent<Item>().ID==ID)
                {
                    weapon = weaponManager.transform.GetChild(i).gameObject;
                }
            }
        }
    }

    public void Update()
    {
        if(equipped)
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                equipped = false;
            }
            if(equipped==false)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void ItemUsage()
    {
        if(type=="Weapon")
        {
            weapon.SetActive(true);
            weapon.GetComponent<Item>().equipped = true; 
        }
        if(type=="Armour")
        {

        }
        if(type=="Health")
        {

        }
    }
}
