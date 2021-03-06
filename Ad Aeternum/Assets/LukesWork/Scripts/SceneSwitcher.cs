﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    Animator transition;
    float transitionTime = 1f;
    GameObject player;
    QuicksaveScript quickSave;

    private void Start()
    {
        transition = GetComponent<Animator>();
        player = GameObject.Find("PlayerMoveController");
        quickSave = GameObject.Find("PlayerMoveController").GetComponent<QuicksaveScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Main Menu Scene
            StartCoroutine(LoadLevel(0));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Main Game/Open World Scene
            StartCoroutine(LoadLevel(1));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Luke's Level
            StartCoroutine(LoadLevel(new Vector3(647.9f, 20, 1237)));
            //StartCoroutine(LoadLevel(2));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //Paulina's Level
            StartCoroutine(LoadLevel(new Vector3(245.7f, -3.2f, -4)));
            //StartCoroutine(LoadLevel(4));
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //Scott's Level
            StartCoroutine(LoadLevel(new Vector3(-10.5f, 6.6f, 624)));
            //StartCoroutine(LoadLevel(5));
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //Jordan's Level
            StartCoroutine(LoadLevel(new Vector3(1710, 273, 550)));
            //StartCoroutine(LoadLevel(3));
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            //Settings Scene
            StartCoroutine(LoadLevel(6));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    public IEnumerator LoadLevel(object obj)
    {
        if (obj is int)
        {
            transition.SetTrigger("Start");

            yield return new WaitForSecondsRealtime(transitionTime);

            SceneManager.LoadScene((int)obj);
        }

        if (obj is Vector3)
        {
            //transition.SetTrigger("Start");

            //yield return new WaitForSecondsRealtime(transitionTime);

            player.transform.position = (Vector3)obj;
            quickSave.targetTime = 0;
            //yield return new WaitForSecondsRealtime(1);

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
