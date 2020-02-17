using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuicksaveScript : MonoBehaviour
{
    public GameObject player, cam, savingText;
    public ArrowCount arrowCount;
    public DataHandler dataHandler;
    public SceneSwitcher sceneSwitcher;
    bool savingBool;
    public float targetTime = 300;

    void Start()
    {
        savingText.SetActive(false);
        player = gameObject;
        dataHandler = GetComponent<DataHandler>();
    }
    
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f || Input.GetKeyDown(KeyCode.F5))
        {
            targetTime = 300;

            dataHandler.data.playerPosition = player.transform.position;
            dataHandler.data.arrowCount = arrowCount.arrowCount;
            dataHandler.data.camPosition = cam.transform.position;
            dataHandler.OnDeathSave();

            CancelInvoke();
            savingBool = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(sceneSwitcher.LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }

        if (savingBool == true)
        {
            savingText.SetActive(true);
            Invoke("Saving", 2);
        }
        else
        {
            savingText.SetActive(false);
        }
    }

    void Saving()
    {
        savingBool = false;
    }
}
