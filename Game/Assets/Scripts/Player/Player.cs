using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, ISpellCaster
{
    public Transform Transform { get; set; }

    private PlayerMovement _movement;

    // Spells
    private ISpell _fireballSpell;

    private void Awake()
    {
        Transform = transform;

        _movement = GetComponent<PlayerMovement>();

        _fireballSpell = new FireballSpell();
    }

    private void Update()
    {
        if (Input.GetButton(InputAxesLiterals.UseSelectedHotSpell))
        {
            _fireballSpell.Cast(this);
        }

        if (Input.GetButton(InputAxesLiterals.UseSelectedColdSpell))
        {
            
        }
    }
}
