using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, ISpellCaster
{
    public Transform Transform { get; set; }

    private PlayerMovement _movement;
    private PlayerWand _wand;

    private void Awake()
    {
        Transform = transform;

        _movement = GetComponent<PlayerMovement>();
        _wand = GetComponentInChildren<PlayerWand>();
    }

    private void Update()
    {
        
    }
}
