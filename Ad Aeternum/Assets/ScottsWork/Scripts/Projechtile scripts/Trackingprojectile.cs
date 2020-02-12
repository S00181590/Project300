using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackingprojectile : NormalProjechtile
{
    GameObject Gametarget;
    GameObject Shooter;
    int Damage;

    Vector3 Direction;

    bool canFire;

    void Update()
    {
        if (Gametarget)
        {
            Direction = Gametarget.transform.position;
        }
        else
        {
            if (transform.position == Direction)
            {
                Destroy(gameObject);
            }
        }

        //transform.position = Vector3.MoveTowards(transform.position, Direction, speed * Time.deltaTime);
    }

    public void    FireProjechtile(GameObject Shoterobjecht, GameObject target, int damage, float attackspeed)
    {
        if (target)
        {
            Gametarget = target;
            Direction = target.transform.position;
            Shooter = Shoterobjecht;
            Damage = damage;
        }
        }
        private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<BaseProjectile>() == null)
            Destroy(gameObject);
    }
}
