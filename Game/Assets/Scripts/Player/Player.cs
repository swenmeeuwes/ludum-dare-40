using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, ISpellCaster
{
    public Transform Transform { get; set; }

    public PlayerWand Wand;
    public PlayerMovement Movement;    

    private void Awake()
    {
        Transform = transform;

        Movement = GetComponent<PlayerMovement>();
        Wand = GetComponentInChildren<PlayerWand>();
    }

    private void Update()
    {
        
    }
}
