using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWand : MonoBehaviour
{
    private Player _player;

    private ISpell _fireballSpell;    

    private void Awake()
    {
        _player = GetComponentInParent<Player>();

        _fireballSpell = new FireballSpell();
    }

    private void Update()
    {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseWandDelta = (mouseWorldPosition - transform.position).normalized;
        var rotationAngle = Mathf.Atan(mouseWandDelta.y / mouseWandDelta.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(Vector3.forward * rotationAngle);

        if (Input.GetButton(InputAxesLiterals.UseSelectedHotSpell))
        {
            _fireballSpell.Cast(_player);
        }

        if (Input.GetButton(InputAxesLiterals.UseSelectedColdSpell))
        {

        }
    }
}
