using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public ArrowShooter arrow;
    public ArrowCount count;
    public bool collectable;

    private void Start()
    {
        arrow = gameObject.GetComponent<ArrowShooter>();
        collectable = false;
    }

    private void Update()
    {
        //transform.localEulerAngles += Vector3.left * 10 * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();

            joint.anchor = collision.contacts[0].point;

            joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();

            joint.enableCollision = false;

            gameObject.transform.parent = collision.gameObject.transform;

            collectable = true;

            Invoke("DestroyArrow", 30);
        }
        else if (collision.collider.tag == "Enemy")
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();

            joint.anchor = collision.contacts[0].point;

            joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();

            joint.enableCollision = false;

            gameObject.transform.parent = collision.gameObject.transform;

            collectable = false;

            Invoke("DestroyArrow", 15);
        }
        //else
        //{
        //    arrow.collectable = false;
        //}

        //arrow.rb.isKinematic = true;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (Input.GetKey(KeyCode.F) && other.tag == "Interactive")
    //    {
    //        count.arrowCount++;
    //        Destroy(gameObject);
    //    }
    //}

    public void DestroyArrow()
    {
        Destroy(gameObject/*arrow.spawnedArrow*/);
    }
}
