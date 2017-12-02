using System;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour
{
    public static T Instance;

    protected void OnDestroy()
    {
        Instance = default(T);
    }

    protected void DefineSingleton(T parent)
    {
        if (Instance != null)
            throw new Exception(string.Format("{0} has already been instantiated. Please make sure you only have one in the scene!", parent.ToString()));

        Instance = parent;
    }    
}