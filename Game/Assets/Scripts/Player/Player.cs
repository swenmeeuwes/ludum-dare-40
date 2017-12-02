using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, ISpellCaster
{
    public Transform Transform { get; set; }

    public PlayerWand Wand;
    private PlayerMovement _movement;    

    private void Awake()
    {
        Transform = transform;

        _movement = GetComponent<PlayerMovement>();
        Wand = GetComponentInChildren<PlayerWand>();
    }

    private void Update()
    {
        
    }
}
