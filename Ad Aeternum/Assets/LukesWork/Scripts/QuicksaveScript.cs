using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuicksaveScript : MonoBehaviour
{
    GameObject player, cam, savingText;
    ArrowCount arrowCount;
    DataHandler dataHandler;
    SceneSwitcher sceneSwitcher;
    bool savingBool;

    [HideInInspector]
    public float targetTime = 300;

    void Start()
    {
        player = gameObject;
        cam = GameObject.Find("Main Camera");
        savingText = GameObject.Find("SavingText");
        arrowCount = GameObject.Find("UICanvas").GetComponent<ArrowCount>();
        dataHandler = GameObject.Find("DataHandler").GetComponent<DataHandler>();
        sceneSwitcher = GameObject.Find("LoadTransition").GetComponent<SceneSwitcher>();

        savingText.SetActive(false);
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
