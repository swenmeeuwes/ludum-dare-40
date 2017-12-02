using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SceneLoadProgess : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (SceneLoader.Instance != null && SceneLoader.Instance.CurrentAsyncOperation != null)
            _slider.value = SceneLoader.Instance.CurrentAsyncOperation.progress;
    }
}
