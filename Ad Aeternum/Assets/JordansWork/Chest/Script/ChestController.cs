using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    float mDistance;
    private GameObject Player;
    private Canvas canvas;
    private Animator animator;
    public bool isOpened;
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        canvas = Camera.main.GetComponent<Canvas>();

        isOpened = false;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mDistance = Vector3.Distance(gameObject.transform.position, Player.transform.position);

        if(mDistance < 4)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isOpened = !isOpened;

                animator.SetBool("IsOpened", isOpened);
            }
        }
        else
        {
            isOpened = false;
            animator.SetBool("IsOpened", isOpened);
        }
    }
}
