using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInvisibleObjects : MonoBehaviour
{
    public LayerMask LayerMask;
    RaycastHit[] hitResults;
    List<string> objectsBlockingView = new List<string>();
    public GameObject player;

    void Update()
    {
        hitResults = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, 1.5f, LayerMask);

        if (hitResults.Length > 0)
        {
            //Hide all blocking objects

            ShowAllObjects();

            for (int i = 0; i < hitResults.Length; i++)
            {
                if (!objectsBlockingView.Contains(hitResults[i].collider.gameObject.name))
                {
                    objectsBlockingView.Add(hitResults[i].collider.gameObject.name);
                    HideObject(hitResults[i].collider.gameObject.name);
                }
            }
        }
        else
        {
            //Show all previously hidden objects

            ShowAllObjects();
        }
    }

    private void ShowAllObjects()
    {
        for (int i = 0; i < objectsBlockingView.Count; i++)
        {
            ShowObject(objectsBlockingView[i]);
        }

        objectsBlockingView.Clear();
    }

    private void HideObject(string name)
    {
        GameObject foundObject = GameObject.Find(name);
        MeshRenderer renderer = foundObject.GetComponent<MeshRenderer>();
        Color originalColour = renderer.material.color;
        originalColour.a = 0.1f;
        renderer.material.color = originalColour;
    }

    private void ShowObject(string name)
    {
        GameObject foundObject = GameObject.Find(name);
        MeshRenderer renderer = foundObject.GetComponent<MeshRenderer>();
        Color originalColour = renderer.material.color;
        originalColour.a = 1.0f;
        renderer.material.color = originalColour;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + (Camera.main.transform.forward * 1.5f));
    }
}