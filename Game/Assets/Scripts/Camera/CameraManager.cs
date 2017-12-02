using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoSingleton<CameraManager>
{
    private Camera _camera;

    private void Awake()
    {
        DefineSingleton(this);

        _camera = GetComponent<Camera>();
    }

    /// <summary>
    /// Shake the camera
    /// </summary>
    /// <param name="duration">Shake duration in seconds</param>
    public void Shake(float duration)
    {
        StartCoroutine(ShakeCoroutine(duration));
    }

    private IEnumerator ShakeCoroutine(float duration)
    {
        var timeSinceShakeStart = Time.time;
        var cameraTransform = _camera.transform;        
        while (Time.time - timeSinceShakeStart < duration)
        {
            cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.x, cameraTransform.rotation.y, Random.value * 3);
            yield return null;
        }

        cameraTransform.transform.rotation = Quaternion.identity;
    }
}
