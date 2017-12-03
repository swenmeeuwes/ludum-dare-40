using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: should the player wand be the ISpellCaster?
public class PlayerWand : MonoEventDispatcher
{
    public static readonly string SpellCast = "PlayerWand.SpellCast";

    private Player _player;

    private ISpell _fireballSpell;
    private ISpell _iceBeamSpell;

    protected override void Awake()
    {
        base.Awake();

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
            Dispatch(new EventObject
            {
                Sender = this,
                Type = SpellCast,
                Data = _fireballSpell
            });
        }

        // Cold spell
        if (Input.GetButton(InputAxesLiterals.UseSelectedColdSpell))
        {
            _iceBeamSpell.Cast(_player);
            Dispatch(new EventObject
            {
                Sender = this,
                Type = SpellCast,
                Data = _iceBeamSpell
            });
        }
        if (Input.GetButtonUp(InputAxesLiterals.UseSelectedColdSpell) && _iceBeamSpell.IsCasting)
            _iceBeamSpell.StopCasting(_player);
    }

    public void StopCastingAll()
    {
        if (_iceBeamSpell.IsCasting)
            _iceBeamSpell.StopCasting(_player);
    }
}
