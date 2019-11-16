using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UserProfile
{
    public string Username;
    public Color Color;
    public string ImageName;

    [NonSerialized]//no going to be saved to JSON
    public Sprite Image;
}
