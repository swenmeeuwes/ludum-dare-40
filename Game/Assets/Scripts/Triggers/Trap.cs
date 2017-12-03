using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour, ITrap
{
    public bool Enabled { get; set; }
    public abstract void Activate();

    protected virtual void Awake()
    {
        Enabled = true;
    }

    protected virtual void Update()
    {
        if (!Enabled)
            Destroy(gameObject);
    }
}
