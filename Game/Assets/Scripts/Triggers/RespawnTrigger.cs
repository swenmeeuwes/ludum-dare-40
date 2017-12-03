using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : TagTrigger
{
    [SerializeField] private Vector3 _respawnPoint;

    protected override void Trigger(Collider2D other)
    {
        base.Trigger(other);

        other.transform.position = _respawnPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_respawnPoint, 0.5f);
    }
}
