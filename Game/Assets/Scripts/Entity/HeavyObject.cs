using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class HeavyObject : MonoBehaviour
{
    private Rigidbody2D _rigidbody;    
    private Collider2D _collider;    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_rigidbody.velocity.magnitude < 2f)
            return;

        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.Hit();
            gameObject.layer = (int) Layers.Deactivated;
        }
    }
}
