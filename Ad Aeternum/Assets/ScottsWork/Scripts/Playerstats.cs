using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerstats : MonoBehaviour
{
    public int MaxHealth = 100;
    public int currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = MaxHealth;
    }

    public void takenDamage ( int damage )
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {

    }
}
