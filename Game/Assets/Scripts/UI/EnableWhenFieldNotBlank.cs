using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableWhenFieldNotBlank : MonoBehaviour
{
    [SerializeField] private Text _textField;
    [SerializeField] private GameObject _gameObject;

    private void Update()
    {
        _gameObject.SetActive(_textField.text.Length > 0);
    }
}
