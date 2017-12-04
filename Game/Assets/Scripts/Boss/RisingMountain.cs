using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingMountain : MonoBehaviour
{
    [SerializeField] private GameObject _mountainObject;
    [SerializeField] private float _riseAmount;
    [SerializeField] private int _steps;
    [SerializeField] private float _timeInSeconds;
    [SerializeField] private bool _cameraShake;

    private float _risePerStep;
    private float _timePerStep; // In seconds
    private int _currentStep;

    private void Awake()
    {
        _risePerStep = _steps / _riseAmount;
        _timePerStep = _timeInSeconds / _steps;
        _mountainObject.transform.position += Vector3.down * _riseAmount;
        _currentStep = 0;
    }

    private void Start()
    {
        StartCoroutine(Rise());
    }

    private IEnumerator Rise()
    {
        while (_currentStep < _steps)
        {
            _mountainObject.transform.position += Vector3.up * _risePerStep;

            if (_cameraShake && CameraManager.Instance != null)
                CameraManager.Instance.Shake(0.2f);

            _currentStep++;

            yield return new WaitForSeconds(_timePerStep);
        }
    }
}
