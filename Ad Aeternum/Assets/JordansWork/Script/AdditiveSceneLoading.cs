using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneLoading : MonoBehaviour
{
    

    private void Awake()
    {
        SceneManager.LoadScene("MountainTop", LoadSceneMode.Additive);
        SceneManager.LoadScene("Forest", LoadSceneMode.Additive);
        SceneManager.LoadScene("CoastSea", LoadSceneMode.Additive);
        SceneManager.LoadScene("Mines", LoadSceneMode.Additive);
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("TownScene", LoadSceneMode.Additive);
        //SceneManager.LoadScene("MountainTop", LoadSceneMode.Additive);
        //SceneManager.LoadScene("CoastSea", LoadSceneMode.Additive);
        //SceneManager.LoadScene("Mines", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "MainHubTransition")
    //    {
    //        SceneManager.LoadScene("TownScene", LoadSceneMode.Additive);
    //    }
    //    else if(other.gameObject.tag == "MountainTransition")
    //    {
    //        SceneManager.LoadScene("MountainTop", LoadSceneMode.Additive);
    //    }
    //    else if (other.gameObject.tag == "SeaTransition")
    //    {
    //        SceneManager.LoadScene("CoastSea", LoadSceneMode.Additive);
    //    }
    //    else if (other.gameObject.tag == "MineTransition")
    //    {
    //        SceneManager.LoadScene("Mines", LoadSceneMode.Additive);
    //    }

    //}
}
