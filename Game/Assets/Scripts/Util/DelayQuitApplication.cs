using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayQuitApplication : MonoBehaviour {
    [SerializeField] private float _secondsTillQuit;

    private void Awake()
    {
        Invoke("Quit", _secondsTillQuit);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
