using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemystats : Playerstats
{
    public override void Die()
    {
        base.Die();

        Destroy(gameObject);

    }
}
