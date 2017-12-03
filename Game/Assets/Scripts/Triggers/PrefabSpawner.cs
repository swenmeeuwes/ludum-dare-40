using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : Spawner
{
    [SerializeField] private GameObject _prefabToSpawn;
    public override void Spawn()
    {
        Instantiate(_prefabToSpawn, transform.position, Quaternion.identity);
    }
}
