using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : TagTrigger
{
    [SerializeField] private Vector3 _respawnPoint;

    protected override void Trigger(Collider2D other)
    {
        base.Trigger(other);

        var player = other.GetComponent<Player>();
        if (player != null)
            player.Hit();

        other.transform.position = _respawnPoint;
    }

    private void OnDrawGizmos()
    {
        var triggerCollider = GetComponent<Collider2D>();
        if (triggerCollider != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(triggerCollider.bounds.center, triggerCollider.bounds.size);            
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_respawnPoint, 0.5f);
    }
}
