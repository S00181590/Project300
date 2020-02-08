using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public ArrowShooter arrow;
    public ArrowCount count;
    public bool collectable;
    public TrailRenderer trail;

    public Camera cam;
    public GameObject arrowObj;
    public Text arrowPickupText;

    public GameObject player;

    private void Start()
    {
        arrow = gameObject.GetComponent<ArrowShooter>();
        collectable = false;
        trail = this.gameObject.GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    private void FixedUpdate()
    {
        trail.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Enemy")
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

        //player.GetComponent<ArrowShooter>().arrowScream.Stop();
    }

    private void OnTriggerStay(Collider other)
    {
        trail.enabled = false;

        if (other.gameObject.tag == "Interactive" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            trail.enabled = false;
        }

        //player.GetComponent<ArrowShooter>().arrowScream.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        trail.enabled = false;

        if (other.gameObject.tag == "Interactive" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            trail.enabled = false;
        }

        //player.GetComponent<ArrowShooter>().arrowScream.Stop();
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
