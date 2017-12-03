using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveStringPlayerPrefButton : MonoBehaviour
{
    [SerializeField] private string _prefKey;
    [SerializeField] private Text _valueTextField;

    public void Save()
    {
        PlayerPrefs.SetString(_prefKey, _valueTextField.text);
    }
}
