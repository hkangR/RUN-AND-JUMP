using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGrayScale : MonoBehaviour
{
    public Material grayscaleMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (grayscaleMaterial != null)
        {
            Graphics.Blit(source, destination, grayscaleMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
