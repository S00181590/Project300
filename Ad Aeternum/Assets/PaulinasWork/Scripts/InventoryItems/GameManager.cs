using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Inventory AllItemsInTheGame;

    public static GameManager Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (GameManager.Instance == null)
            GameManager.Instance = this;
    }
}
