using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public ArrowShooter arrow;

    private void Start()
    {
        arrow = gameObject.GetComponent<ArrowShooter>();
        string destroy = "arrow.DestroyArrow";
    }

    private void Update()
    {
        //transform.localEulerAngles += -Vector3.left * 200 * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Enemy")
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();

            joint.anchor = collision.contacts[0].point;

            joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();

            joint.enableCollision = false;

            Invoke("DestroyArrow", 3);
        }

        arrow.rb.isKinematic = true;
        
    }

    public void DestroyArrow()
    {
        
        Destroy(arrow.spawnedArrow);
    }
}
