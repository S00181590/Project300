using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : NavMeshMover
{
    public int health;
    public int damage;

    public Animator animator;
    // Start is called before the first frame update
    public override void Start()
    {
        animator = GetComponent<Animator>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (health <=0)
        {
            if(health <=50)
            {
                animator.SetTrigger("Punch");//when the boss hp drops to 50 it switches to the punch animaton 
            }
            if (health <= 0)
            {
                animator.SetTrigger("death");//when the boss hp drops to 50 it switches to the punch animaton 
            }
        }
    }
}
