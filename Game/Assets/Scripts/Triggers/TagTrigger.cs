using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TagTrigger : MonoBehaviour
{
    [SerializeField] protected Tags Tag;
    [SerializeField] protected UnityEvent OnTrigger;

    protected Collider2D _collider;

    protected virtual void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tag.ToString())
            Trigger(other);
    }

    protected virtual void Trigger(Collider2D other)
    {
        OnTrigger.Invoke();
    }
}
