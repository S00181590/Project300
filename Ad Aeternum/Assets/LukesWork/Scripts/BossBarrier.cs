using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarrier : MonoBehaviour
{
    public string bossName;
    GameObject boss;

    void Start()
    {
        boss = GameObject.Find(bossName);
    }
    
    void Update()
    {
        if (boss == null)
        {
            Destroy(this.gameObject);
        }
    }
}
