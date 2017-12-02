using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// The most shitties brush ever, todo research brushes later
[CustomGridBrush(true, false, false, "PrefabBrush Brush")]
public class PrefabBrush : GridBrush
{
    [SerializeField] private GameObject _prefab;

    public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
    {
        if (_prefab == null)
            return;

        var correctedPosition = position + Vector3.one * 0.5f;

        var possibleHit = Physics2D.OverlapPoint(correctedPosition);
        if (possibleHit != null)
            return;

        var instance = Instantiate(_prefab);
        instance.transform.position = correctedPosition;
    }
}
