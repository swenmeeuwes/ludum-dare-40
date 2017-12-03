using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CollectableType
{
    IceBeamTome,
    FireBallTome
}

public interface ICollectable
{
    void Collect();
}

public class Collectable : MonoBehaviour, ICollectable
{
    public CollectableType Type;

    [SerializeField] private UnityEvent _onCollect;

    public void Collect()
    {
        _onCollect.Invoke();
        Destroy(gameObject);
    }
}
