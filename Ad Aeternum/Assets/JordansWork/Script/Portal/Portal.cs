using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    public MeshRenderer Screen;
    Camera playerCam, portalCam;
    RenderTexture viewTexture;

    void Awake()
    {
        playerCam = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
        portalCam.enabled = false;
    }

    void CreateViewTexture()
    {
        if(viewTexture == null || viewTexture.width != Screen.bounds.size.x || 
                                  viewTexture.height != Screen.bounds.size.y)
        {
            viewTexture.Release();
        }

        viewTexture = new RenderTexture((int)Screen.bounds.size.x, 
                                        (int)Screen.bounds.size.y, 0);

        portalCam.targetTexture = viewTexture;

        linkedPortal.Screen.material.SetTexture("_MainTex", viewTexture);
    }

    public void Render()
    {
        Screen.enabled = false;
        CreateViewTexture();

        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * playerCam.transform.localToWorldMatrix;

        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        portalCam.Render();

        Screen.enabled = true;

    }
}
