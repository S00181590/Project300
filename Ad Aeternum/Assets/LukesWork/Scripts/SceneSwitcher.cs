using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(LoadLevel(0));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(LoadLevel(1));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(LoadLevel(2));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(LoadLevel(3));
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartCoroutine(LoadLevel(4));
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            StartCoroutine(LoadLevel(5));
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StartCoroutine(LoadLevel(6));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
