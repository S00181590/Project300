using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateScript : MonoBehaviour
{
    public List<GameObject> enemies;
    List<bool> enemiesBool;
    GameObject barrier;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    void Start()
    {
        barrier = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (GameObject enemy in enemies)
        //{
        //    if (enemy == null)
        //    {
        //        enemiesBool[enemy.GetComponent<List>().Count(enemy)] = true;
        //    }
        //}

        //if (!enemiesBool.Contains(false))
        //{
        //    Destroy(barrier);
        //}

        if (enemy1 == null & enemy2 == null && enemy3 == null)
        {
            Destroy(barrier);
        }
    }
}
