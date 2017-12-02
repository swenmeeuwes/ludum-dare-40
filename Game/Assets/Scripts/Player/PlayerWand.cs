using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: should the player wand be the ISpellCaster?
public class PlayerWand : MonoBehaviour
{
    private Player _player;

    private ISpell _fireballSpell;
    private ISpell _iceBeamSpell;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();

        _fireballSpell = new FireballSpell();
        _iceBeamSpell = new IceBeamSpell();       
    }

    private void Update()
    {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseWandDelta = (mouseWorldPosition - transform.position).normalized;
        var rotationAngle = Mathf.Atan(mouseWandDelta.y / mouseWandDelta.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(Vector3.forward * rotationAngle);

        // Hot spell
        if (Input.GetButton(InputAxesLiterals.UseSelectedHotSpell))
        {
            _fireballSpell.Cast(_player);
        }

        // Cold spell
        if (Input.GetButton(InputAxesLiterals.UseSelectedColdSpell))
        {
            _iceBeamSpell.Cast(_player);
        }
        if (Input.GetButtonUp(InputAxesLiterals.UseSelectedColdSpell))
            _iceBeamSpell.StopCasting(_player);
    }
}
