using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDistanceTrap : Trap
{
    [Tooltip("The trap will activate when the distance between the player and the trap is less.")]
    [SerializeField] protected float Distance;

    protected Player Player;

    protected virtual void Start()
    {
        Player = FindObjectOfType<Player>();
    }

    protected override void Update()
    {
        base.Update();

        if (!Enabled)
            return;

        if (Vector2.Distance(transform.position, Player.transform.position) < Distance)        
            Activate();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Distance);
    }
}
