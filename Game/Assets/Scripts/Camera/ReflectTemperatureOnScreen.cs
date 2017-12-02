using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectTemperatureOnScreen : MonoBehaviour {
    [SerializeField] private Material _fadeToTemperatureColorMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_fadeToTemperatureColorMaterial != null && TemperatureManager.Instance != null)
        {
            _fadeToTemperatureColorMaterial.SetFloat("_Temperature", TemperatureManager.Instance.Temperature);
            Graphics.Blit(source, destination, _fadeToTemperatureColorMaterial);
        }
    }
}
