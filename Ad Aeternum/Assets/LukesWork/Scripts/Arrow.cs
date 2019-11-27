using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public ArrowShooter arrow;
    public ArrowCount count;
    public bool collectable;
    public TrailRenderer trail;

    private void Start()
    {
        arrow = gameObject.GetComponent<ArrowShooter>();
        collectable = false;
        trail = this.gameObject.GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    private void FixedUpdate()
    {
        //transform.localEulerAngles += Vector3.left * 10 * Time.deltaTime
        trail.enabled = true;
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

            trail.enabled = false;

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

            trail.enabled = false;

            Invoke("DestroyArrow", 15);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Interactive" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            trail.enabled = false;
        }
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
