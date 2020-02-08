using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateScript : MonoBehaviour
{
    public List<GameObject> enemies;
    List<bool> enemiesBool;
    GameObject barrier;
    public ParticleSystem particles;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    public GameObject enemy7;
    public GameObject enemy8;
    public GameObject enemy9;

    void Start()
    {
        barrier = this.gameObject;
    }

    public void Particle(Vector3 position)
    {
        Instantiate(particles, position, Quaternion.identity);
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

        if (enemy1 == null & enemy2 == null && enemy3 == null && enemy4 == null && enemy5 == null
            && enemy6 == null && enemy7 == null && enemy8 == null && enemy9 == null)
        {
            Particle(barrier.transform.position);
            Destroy(barrier);
            
        }
    }
}
