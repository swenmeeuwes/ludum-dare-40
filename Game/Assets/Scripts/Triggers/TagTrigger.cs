using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TagTrigger : MonoBehaviour
{
    [SerializeField] private Tags _tag;
    [SerializeField] private UnityEvent _onTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == _tag.ToString())
            _onTrigger.Invoke();
    }
}
