using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public int ID;//unique
    public string Name;
    public string Description;
    public float Value;
    public string IconName;

    [NonSerialized]//wont be saved to JSON
    public Sprite Icon;
}
