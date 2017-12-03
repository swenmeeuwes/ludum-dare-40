using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGameObject : MonoBehaviour
{
    [SerializeField] private GameObject[] _controlledGameObjects;
    [SerializeField] private bool _disableByDefault = true;

    private void Awake()
    {
        if (_disableByDefault)
            DisableGameObjects();
    }

    public void DisableGameObjects()
    {
        foreach (var controlledGameObject in _controlledGameObjects)
        {
            controlledGameObject.SetActive(false);
        }            
    }

    public void EnableGameObjects()
    {
        foreach (var controlledGameObject in _controlledGameObjects)
        {
            controlledGameObject.SetActive(true);
        }
    }
}
