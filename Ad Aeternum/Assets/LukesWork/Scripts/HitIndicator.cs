using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitIndicator : MonoBehaviour
{
    public GameObject hitIndicator = null, hitInst = null;
    public Transform parent;


    private Vector3 screenPos;
    public Camera cam;

    public Text damageAmountText = null;
    public int damageAmount;

    bool active = false;

    void Start()
    {
        //hitIndicator.SetActive(false);
        damageAmount = 150;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (active && hitInst)
        {
            screenPos = cam.WorldToScreenPoint(Vector3.Lerp(gameObject.transform.position, new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y + 100,
            gameObject.transform.position.z),
            0.1f * Time.deltaTime));

            hitIndicator.transform.position = screenPos;
            hitInst.transform.position = screenPos;

            damageAmountText.text = "-" + damageAmount.ToString();

            hitInst.transform.localScale += new Vector3(0.2f, 0.1f, 0.1f) * -15 * Time.deltaTime;
            //hitInst.transform.position = Vector3.Lerp(hitIndicator.transform.position, new Vector3(hitIndicator.transform.position.x, hitIndicator.transform.position.y, hitIndicator.transform.position.z), 0.1f);

            Destroy(hitInst, 3);
            //Invoke("SetUnactive", 3);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Arrow")
        {
            if (hitInst)
                Destroy(hitInst);

            hitInst = Instantiate(hitIndicator, parent);
            hitInst.transform.localScale = new Vector3(10, 5, 1);
            hitInst.SetActive(true);

            active = true;
        }
    }

    void SetUnactive(GameObject hit)
    {
        if (hit)
        {
            active = false;
        }
    }
}
