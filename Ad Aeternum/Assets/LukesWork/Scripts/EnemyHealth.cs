using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health = 1000, damage = 50;
    public GameObject healthSlider;
    bool letPlay = false;
    public ParticleSystem prticleSystem;
    ParticleSystem instantiate;
    public GameObject particleObj;

    private void Start()
    {
        healthSlider = transform.Find("Canvas").Find("EnemyHealthSlider").gameObject;
        prticleSystem.Stop();
    }

    void Update()
    {
        healthSlider.GetComponent<Slider>().value = health;

        if (healthSlider.GetComponent<Slider>().value <= 0)
        {
            letPlay = true;

            if (gameObject != null)
            {
                particleObj.transform.position = gameObject.transform.position;
            }

            Destroy(this.gameObject, 1.5f);
        }

        if (letPlay)
        {
            if (!prticleSystem.isPlaying)
            {
                instantiate = Instantiate(prticleSystem);
                instantiate.transform.position = gameObject.transform.position;
                prticleSystem.Play();
            }
        }
        //else
        //{
        //    if (prticleSystem.isPlaying)
        //    {
        //        prticleSystem.Stop();
        //    }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            health -= damage;
        }
    }
}
