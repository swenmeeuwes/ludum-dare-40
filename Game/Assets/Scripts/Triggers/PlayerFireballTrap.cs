using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireballTrap : PlayerDistanceTrap
{
    [SerializeField] private Vector2 _fireballVelocity;

    public override void Activate()
    {        
        var fireballObject = Instantiate(PrefabLocator.Instance.Locate(Prefab.Fireball), transform.position, Quaternion.identity);
        var fireball = fireballObject.GetComponent<Fireball>();
        fireball.SetVelocity(_fireballVelocity);
        fireball.SetAngularDrag(1f);

        Enabled = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)_fireballVelocity);
    }
}
