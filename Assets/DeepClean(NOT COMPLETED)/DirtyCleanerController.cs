using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyCleanerController : MonoBehaviour
{
    private Camera _mainCamera;
    public Texture2D texture;
    void Start()
    {
        onInitialize();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50f, 1 << 7))
            {
                texture = hit.collider.GetComponent<Renderer>().material.mainTexture as Texture2D;
                Vector2 UVCoords = hit.textureCoord;
                UVCoords.x *= texture.width;
                UVCoords.y *= texture.height;
                paintRadius(texture, (int)UVCoords.x, (int)UVCoords.y);
            }
        }
    }

    private void onInitialize()
    {
        _mainCamera = Camera.main;
    }

    private void paintRadius(Texture2D texture, int x, int y)
    {
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                texture.SetPixel(x-15+i, y-15+j, Color.red);
                texture.Apply();
            }
        }
        
    }
}
