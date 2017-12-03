using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeInputField : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private string[] _options;
    [SerializeField] private bool _setTextOnStart;

    private void Start()
    {
        if (_options.Length == 0 || !_setTextOnStart)
            return;

        _inputField.text = _options[Mathf.FloorToInt(Random.value * (_options.Length - 1))];
    }

    public void RandomizeInputText()
    {
        if (_options.Length == 0)
            return;

        _inputField.text = _options[Mathf.FloorToInt(Random.value * (_options.Length - 1))];
    }
}
