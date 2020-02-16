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

    void Start()
    {
        savingText.SetActive(false);
        player = gameObject;
        dataHandler = GetComponent<DataHandler>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
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
            Invoke("Saving", 3);
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
