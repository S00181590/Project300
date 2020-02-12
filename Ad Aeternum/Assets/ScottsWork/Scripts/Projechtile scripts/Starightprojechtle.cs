using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starightprojechtle : BaseProjectile
{
    public float Projechtilelegnth = 8.0f;

    GameObject mlaucher;
    GameObject Gametarget;
    GameObject Shooter;
    int Damage;
    Vector3 Direction;
    float Shootertimer;
    float AttackSpeed;


    void Update()
    {
        Shootertimer += Time.deltaTime;

        if (mlaucher)
        {
            GetComponent<LineRenderer>().SetPosition(0, mlaucher.transform.position);
            GetComponent<LineRenderer>().SetPosition(1, mlaucher.transform.position + (mlaucher.transform.forward * Projechtilelegnth));
            RaycastHit hit;


            if  (Physics.Raycast(Shooter.transform.position, Shooter.transform.forward, out hit, Projechtilelegnth))
                {
                        if(Shootertimer >= AttackSpeed)
                           {

                             }
                }

        }
    }
        

    public override void fireProjechtile(GameObject Shoterobjecht, GameObject target, int damage, float attackrate)//used also in the shooting class 
    {
        if (Shoterobjecht)
        {
            mlaucher = Shoterobjecht;
            Shooter = target;
            Damage = damage;
            AttackSpeed = attackrate;
            Shootertimer = 0.0f;
        }
    }
}
