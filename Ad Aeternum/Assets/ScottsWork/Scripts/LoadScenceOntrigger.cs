using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenceOntrigger : MonoBehaviour
{
    public string SceneName;
   

    //loading the scence by trigger 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            SceneManager.LoadScene(SceneName);
        }
      
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(sceneBuildIndex: 3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(sceneBuildIndex: 4);
        }
         else if  (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(sceneBuildIndex: 5);
        }
    }

}
