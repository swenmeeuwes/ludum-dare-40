using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dirty unlock system
public class UnlockManager : MonoSingleton<UnlockManager>
{
    public bool UnlockedIceBeam = false;
    public bool UnlockedFireBall = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DefineSingleton(this);
        DontDestroyOnLoad(gameObject);
    }
}
