using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Vector3 position, diff;
    public Transform target;

    public GameObject healthBar;
    List<GameObject> enemies;
    public Canvas canvas;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            GameObject instantiate;

            if (enemy.GetComponent<Renderer>().isVisible)
            {
                instantiate = Instantiate(healthBar, canvas.transform);
                instantiate.transform.localPosition = new Vector3(0, 1, 0);
            }
        }
    }

    //public GameObject GetNearbyEnemies()
    //{
    //    GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
    //    float distance = Mathf.Infinity;
    //    position = target.position;

    //    foreach (GameObject go in gos)
    //    {
    //        diff = go.transform.position - position;
    //        float curDistance = diff.sqrMagnitude;

            
    //    }

    //    return closest;
    //}
}
