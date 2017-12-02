using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToColor : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Material _fadeToColorMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_fadeToColorMaterial != null)
            Graphics.Blit(source, destination, _fadeToColorMaterial);
    }
}
