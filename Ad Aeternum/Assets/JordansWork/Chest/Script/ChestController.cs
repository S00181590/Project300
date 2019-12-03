using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    float mDistance;
    private GameObject Player;
    private GameObject canvas;
    private Animator animator;
    public bool isOpened, InRange;
    public GameObject InventoryPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        canvas = GameObject.FindGameObjectWithTag("Canvas");

        isOpened = false;

        InRange = false;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mDistance = Vector3.Distance(gameObject.transform.position, Player.transform.position);

        if(InRange == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isOpened = !isOpened;
                if(isOpened == true)
                {
                    animator.SetBool("IsOpened", isOpened);
           
                    Instantiate(InventoryPanel, canvas.transform);
                
                }
                else if(isOpened == false)
                {
                    animator.SetBool("IsOpened", isOpened);
                    Destroy(GameObject.FindGameObjectWithTag("ChestInventoryUI"));
                }
            }

            
        }
        else
        {
            isOpened = false;
            animator.SetBool("IsOpened", isOpened);

            Destroy(GameObject.FindGameObjectWithTag("ChestInventoryUI"));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InRange = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InRange = false;

            Destroy(GameObject.FindGameObjectWithTag("ChestInventoryUI"));
        }
    }
}
